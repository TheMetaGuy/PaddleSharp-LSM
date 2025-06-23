﻿using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Sdcb.PaddleOCR;

/// <summary>
/// Represents a PaddleOCR table recognizer.
/// </summary>
public class PaddleOcrTableRecognizer : IDisposable
{
    readonly PaddlePredictor _p;

    /// <summary>
    /// Gets or sets the maximum edge size.
    /// </summary>
    public int MaxEdgeSize { get; set; } = 488;

    /// <summary>
    /// Gets the table recognition model.
    /// </summary>
    public TableRecognitionModel Model { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaddleOcrTableRecognizer"/> class.
    /// </summary>
    /// <param name="model">The table recognition model.</param>
    /// <param name="configure">The action to configure Paddle device.</param>
    public PaddleOcrTableRecognizer(TableRecognitionModel model, Action<PaddleConfig>? configure = null) : this(model, 
        model.CreateConfig().Apply(configure ?? PaddleDevice.PlatformDefault).CreatePredictor())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaddleOcrTableRecognizer"/> class from directory path and label path.
    /// </summary>
    /// <param name="directoryPath">The directory path.</param>
    /// <param name="labelPath">The label path.</param>
    /// <param name="configure">The action to configure Paddle device.</param>
    public PaddleOcrTableRecognizer(string directoryPath, string labelPath, Action<PaddleConfig>? configure = null) : this(TableRecognitionModel.FromDirectory(directoryPath, labelPath), configure)
    {
    }

    private PaddleOcrTableRecognizer(TableRecognitionModel model, PaddlePredictor predictor)
    {
        Model = model;
        _p = predictor;
    }

    /// <summary>
    /// Clones a new <see cref="PaddleOcrTableRecognizer"/> instance.
    /// </summary>
    /// <returns>A new instance of the <see cref="PaddleOcrTableRecognizer"/> class.</returns>
    public PaddleOcrTableRecognizer Clone() => new(Model, _p.Clone());

    /// <summary>
    /// Disposes the PaddleOCR table recognizer.
    /// </summary>   
    public void Dispose() => _p.Dispose();

    /// <summary>
    /// Runs table detection on the image.
    /// </summary>
    /// <param name="src">The input image to run table detection on.</param>
    /// <returns>The table detection result.</returns>
    /// <exception cref="ArgumentException">Thrown when the input image size is 0.</exception>
    /// <exception cref="NotSupportedException">Thrown when the input image channel is not 3 or 1.</exception>
    /// <exception cref="Exception">Thrown when the PaddlePredictor(Table) run failed.</exception>
    public TableDetectionResult Run(Mat src)
    {
        if (src.Empty())
        {
            throw new ArgumentException("src size should not be 0, wrong input picture provided?");
        }

        if (!(src.Channels() switch { 3 or 1 => true, _ => false }))
        {
            throw new NotSupportedException($"{nameof(src)} channel must be 3 or 1, provided {src.Channels()}.");
        }

        Size rawSize = src.Size();
        float[] inputData = TablePreprocess(src);

        PaddlePredictor predictor = _p;
        lock (predictor)
        {
            using (PaddleTensor input = predictor.GetInputTensor(predictor.InputNames[0]))
            {
                input.Shape = new[] { 1, 3, MaxEdgeSize, MaxEdgeSize };
                input.SetData(inputData);
            }
            if (!predictor.Run())
            {
                throw new Exception("PaddlePredictor(Table) run failed.");
            }

            string[] outputNames = predictor.OutputNames;
            using (PaddleTensor output0 = predictor.GetOutputTensor(outputNames[0]))
            using (PaddleTensor output1 = predictor.GetOutputTensor(outputNames[1]))
            {
                float[] locations = output0.GetData<float>();
                int[] locationShape = output0.Shape;
                float[] structures = output1.GetData<float>();
                int[] structureShape = output1.Shape;

                return TablePostProcessor(locations, locationShape, structures, structureShape, rawSize, Model.GetLabelByIndex);
            }
        }
    }

    private float[] TablePreprocess(Mat src)
    {
        using Mat resized = MatResize(src, MaxEdgeSize);
        using Mat normalized = Normalize(resized);
        using Mat padded = MatPadingTo(normalized, MaxEdgeSize);
        return ExtractMat(padded);

        static Mat MatResize(Mat src, int maxSize)
        {
            int w = src.Width;
            int h = src.Height;

            float ratio = w >= h ? (float)maxSize / w : (float)maxSize / h;
            int resizeH = (int)(h * ratio);
            int resizeW = (int)(w * ratio);
            return src.Resize(new Size(resizeW, resizeH));
        }

        static float[] ExtractMat(Mat src)
        {
            int rows = src.Rows;
            int cols = src.Cols;
            float[] result = new float[rows * cols * 3];
            GCHandle resultHandle = default;
            try
            {
                resultHandle = GCHandle.Alloc(result, GCHandleType.Pinned);
                IntPtr resultPtr = resultHandle.AddrOfPinnedObject();
                for (int i = 0; i < src.Channels(); ++i)
                {
                    using Mat dest = Mat.FromPixelData(rows, cols, MatType.CV_32FC1, resultPtr + i * rows * cols * sizeof(float));
                    Cv2.ExtractChannel(src, dest, i);
                }
            }
            finally
            {
                resultHandle.Free();
            }
            return result;
        }

        static Mat MatPadingTo(Mat src, int maxSize)
        {
            Size size = src.Size();
            Size newSize = new(maxSize, maxSize);
            return src.CopyMakeBorder(0, newSize.Height - size.Height, 0, newSize.Width - size.Width, BorderTypes.Constant, Scalar.Black);
        }

        static Mat Normalize(Mat src)
        {
            using Mat normalized = new();
            src.ConvertTo(normalized, MatType.CV_32FC3, 1.0 / 255);
            Mat[] bgr = normalized.Split();
            float[] scales = new[] { 1 / 0.229f, 1 / 0.224f, 1 / 0.225f };
            float[] means = new[] { 0.485f, 0.456f, 0.406f };
            for (int i = 0; i < bgr.Length; ++i)
            {
                bgr[i].ConvertTo(bgr[i], MatType.CV_32FC1, 1.0 * scales[i], (0.0 - means[i]) * scales[i]);
            }

            Mat dest = new();
            Cv2.Merge(bgr, dest);

            foreach (Mat channel in bgr)
            {
                channel.Dispose();
            }

            return dest;
        }
    }

    private static TableDetectionResult TablePostProcessor(
        float[] locations, int[] locationShape,
        float[] structures, int[] structureShape,
        Size rawSize,
        Func<int, string> labelAccessor)
    {
        float score = 0.0f;
        int count = 0;
        List<string> recHtmlTags = new();
        List<TableCellBox> recBoxes = new();

        ReadOnlySpan<float> structureSpan = structures.AsSpan();
        for (int stepIndex = 0; stepIndex < structureShape[1]; ++stepIndex)
        {
            List<int> recBox = new();
            string htmlTag;
            // html tag
            {
                int stepStartIndex = stepIndex * structureShape[2];
                (int charIndex, float charScore) = ArgMax(structureSpan[stepStartIndex..(stepStartIndex + structureShape[2])]);
                htmlTag = labelAccessor(charIndex);
                if (stepIndex > 0 && htmlTag == TableRecognitionModelConsts.LastLabel)
                {
                    break;
                }
                if (htmlTag == TableRecognitionModelConsts.FirstLabel)
                {
                    continue;
                }
                count += 1;
                score += charScore;
                recHtmlTags.Add(htmlTag);
            }

            // box
            if (htmlTag == "<td>" || htmlTag == "<td" || htmlTag == "<td></td>")
            {
                for (int pointIndex = 0; pointIndex < locationShape[2]; pointIndex++)
                {
                    int stepStartIndex = stepIndex * locationShape[2] + pointIndex;
                    float point = locations[stepStartIndex];
                    if (pointIndex % 2 == 0)
                    {
                        point *= rawSize.Width;
                    }
                    else
                    {
                        point *= rawSize.Height;
                    }
                    recBox.Add((int)point);
                }
                recBoxes.Add(new TableCellBox(recBox));
            }
        }

        score /= count;
        if (float.IsNaN(score) || recBoxes.Count == 0)
        {
            score -= 1;
        }
        return new TableDetectionResult(score, recBoxes, recHtmlTags);
    }

    private static (int, float) ArgMax(ReadOnlySpan<float> collection)
    {
        int maxIndex = -1;
        float maxValue = float.NegativeInfinity;
        for (int i = 0; i < collection.Length; i++)
        {
            float value = collection[i];
            if (maxIndex == -1 || value > maxValue)
            {
                maxIndex = i;
                maxValue = value;
            }
        }
        return (maxIndex, maxValue);
    }
}
