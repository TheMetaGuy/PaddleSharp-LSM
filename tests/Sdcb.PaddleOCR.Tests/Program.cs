﻿using OpenCvSharp;
using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR.Models.Online;

//[assembly: Xunit.CollectionBehavior(DisableTestParallelization = true)]

namespace Sdcb.PaddleOCR.Tests;

internal class Program
{
    public static async Task Main()
    {
        Environment.SetEnvironmentVariable("GLOG_v", "0");
        FullOcrModel model = await OnlineFullModels.ChineseV5.DownloadAsync();
        FastCheck(model);

        model = await OnlineFullModels.EnglishV5.DownloadAsync();
        FastCheck(model);
    }

    private static void FastCheck(FullOcrModel model)
    {
        // from: https://visualstudio.microsoft.com/wp-content/uploads/2021/11/Home-page-extension-visual-updated.png
        byte[] sampleImageData = File.ReadAllBytes(@"./samples/vsext.png");

        using (PaddleOcrAll all = new(model)
        {
            AllowRotateDetection = true,
            Enable180Classification = true,
        })
        {
            // Load local file by following code:
            // using (Mat src2 = Cv2.ImRead(@"C:\test.jpg"))
            using (Mat src = Cv2.ImDecode(sampleImageData, ImreadModes.Color))
            {
                PaddleOcrResult result = all.Run(src);
                Console.WriteLine("Detected all texts: \n" + result.Text);
                foreach (PaddleOcrResultRegion region in result.Regions)
                {
                    Console.WriteLine($"Text: {region.Text}, Score: {region.Score}, RectCenter: {region.Rect.Center}, RectSize:    {region.Rect.Size}, Angle: {region.Rect.Angle}");
                }
            }
        }
    }
}
