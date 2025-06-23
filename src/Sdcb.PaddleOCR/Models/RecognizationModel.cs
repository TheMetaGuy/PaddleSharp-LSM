﻿using Sdcb.PaddleInference;
using Sdcb.PaddleOCR.Models.Details;
using System;
using System.Collections.Generic;

namespace Sdcb.PaddleOCR.Models;

/// <summary>
/// The abstract base class for recognition model.
/// </summary>
public abstract class RecognizationModel : OcrBaseModel
{
    /// <summary>
    /// Constructor for initializing an instance of the <see cref="RecognizationModel"/> class.
    /// </summary>
    /// <param name="version">The version of recognition model.</param>
    public RecognizationModel(ModelVersion version) : base(version)
    {
    }

    /// <summary>
    /// Get the label by specifying the index.
    /// </summary>
    /// <param name="i">The index of the label.</param>
    /// <returns>The label with the specified index.</returns>
    public abstract string GetLabelByIndex(int i);

    /// <summary>
    /// Gets a label by its index.
    /// </summary>
    /// <param name="i">The index of the label.</param>
    /// <param name="labels">The labels to search for the index.</param>
    /// <returns>The label at the specified index.</returns>
    public static string GetLabelByIndex(int i, IReadOnlyList<string> labels)
    {
        return i switch
        {
            var x when x > 0 && x <= labels.Count => labels[x - 1],
            var x when x == labels.Count + 1 => " ",
            _ => throw new Exception($"Unable to GetLabelByIndex: index {i} out of range {labels.Count}, OCR model or labels not matched?"),
        };
    }

    /// <summary>
    /// Get the OcrShape of recognition model.
    /// </summary>
    public virtual OcrShape Shape => Version switch
    {
        ModelVersion.V2 => new(3, 320, 32),
        ModelVersion.V3 => new(3, 320, 48),
        ModelVersion.V4 => new(3, 320, 48),
        ModelVersion.V5 => new(3, 320, 48),
        _ => throw new ArgumentOutOfRangeException($"Unknown OCR model version: {Version}."),
    };

    /// <summary>
    /// Gets the default device for the classification model.
    /// </summary>
    public override Action<PaddleConfig> DefaultDevice => PaddleDevice.Mkldnn();

    /// <inheritdoc/>
    public override void ConfigureDevice(PaddleConfig config, Action<PaddleConfig>? configure = null)
    {
        base.ConfigureDevice(config, configure);
        if (config.MkldnnEnabled)
        {
            if (Version == ModelVersion.V3 || Version == ModelVersion.V4)
            {
                config.DeletePass("matmul_transpose_reshape_fuse_pass");

                // https://github.com/PaddlePaddle/Paddle/issues/55290#issuecomment-1629924892
                config.DeletePass("fc_mkldnn_pass");
                config.DeletePass("fc_act_mkldnn_fuse_pass");
            }
        }
    }

    /// <summary>
    /// Create the RecognizationModel object with the specified directory path, label path and model version.
    /// </summary>
    /// <param name="directoryPath">The directory path of recognition model.</param>
    /// <param name="labelPath">The label path of recognition model.</param>
    /// <param name="version">The version of recognition model.</param>
    /// <returns>The RecognizationModel object created with the specified directory path, label path and model version.</returns>
    public static RecognizationModel FromDirectory(string directoryPath, string labelPath, ModelVersion version) => new FileRecognizationModel(directoryPath, labelPath, version);

    /// <summary>
    /// Create the RecognizationModel object with the V5 model directory path.
    /// </summary>
    /// <param name="directoryPath">The directory path of recognition model.</param>
    /// <returns>The RecognizationModel object created with the specified V5 directory path.</returns>
    public static RecognizationModel FromDirectoryV5(string directoryPath) => FromDirectory(directoryPath, "", ModelVersion.V5);
}
