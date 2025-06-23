# Sdcb.PaddleOCR

## PaddleOCR packages 📖

| NuGet Package 💼               | Version 📌                                                                                                                              | Description 📚                                                |
| ----------------------------- | -------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------ |
| Sdcb.PaddleOCR                | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleOCR.svg)](https://nuget.org/packages/Sdcb.PaddleOCR)                               | PaddleOCR library(based on Sdcb.PaddleInference) ⚙️           |
| Sdcb.PaddleOCR.Models.Online  | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleOCR.Models.Online.svg)](https://nuget.org/packages/Sdcb.PaddleOCR.Models.Online)   | Online PaddleOCR models, will download when first using 🌐    |
| Sdcb.PaddleOCR.Models.Local   | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleOCR.Models.Local.svg)](https://nuget.org/packages/Sdcb.PaddleOCR.Models.Local)     | Local models, relies on Shared, LocalV3 and LocalV4 models 🏠 |

## Language supports

Please refer to https://github.com/PaddlePaddle/PaddleOCR/blob/release/2.5/doc/doc_en/models_list_en.md to check language support models.

Just replace the `.ChineseV3` in demo code with your speicific language, then you can use the language.

## Usage

### Windows(Local model): Detection and Recognition(All)
1. Install NuGet Packages:
   ```
   Sdcb.PaddleInference
   Sdcb.PaddleOCR
   Sdcb.PaddleOCR.Models.Local
   Sdcb.PaddleInference.runtime.win64.mkl
   OpenCvSharp4.runtime.win
   ```

2. Using following C# code to get result:
   ```csharp
   FullOcrModel model = LocalFullModels.ChineseV3;
   
   byte[] sampleImageData;
   string sampleImageUrl = @"https://www.tp-link.com.cn/content/images2017/gallery/4288_1920.jpg";
   using (HttpClient http = new HttpClient())
   {
       Console.WriteLine("Download sample image from: " + sampleImageUrl);
       sampleImageData = await http.GetByteArrayAsync(sampleImageUrl);
   }
   
   using (PaddleOcrAll all = new PaddleOcrAll(model, PaddleDevice.Mkldnn())
   {
       AllowRotateDetection = true, /* 允许识别有角度的文字 */ 
       Enable180Classification = false, /* 允许识别旋转角度大于90度的文字 */
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
   ```

### Windows(Online model): Detection and Recognition(All)
1. Install NuGet Packages:
   ```
   Sdcb.PaddleInference
   Sdcb.PaddleOCR
   Sdcb.PaddleOCR.Models.Online
   Sdcb.PaddleInference.runtime.win64.mkl
   OpenCvSharp4.runtime.win
   ```

2. Using following C# code to get result:
   ```csharp
   FullOcrModel model = await OnlineFullModels.EnglishV3.DownloadAsync();
   
   byte[] sampleImageData;
   string sampleImageUrl = @"https://www.tp-link.com.cn/content/images2017/gallery/4288_1920.jpg";
   using (HttpClient http = new HttpClient())
   {
       Console.WriteLine("Download sample image from: " + sampleImageUrl);
       sampleImageData = await http.GetByteArrayAsync(sampleImageUrl);
   }
   
   using (PaddleOcrAll all = new PaddleOcrAll(model, PaddleDevice.Mkldnn())
   {
       AllowRotateDetection = true, /* 允许识别有角度的文字 */ 
       Enable180Classification = false, /* 允许识别旋转角度大于90度的文字 */
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
   ```

### non-Windows support
Just install the following NuGet packages, and run the same code as Windows:
```bash
# For linux-x64
Install-Package Sdcb.PaddleInference.runtime.linux-x64.mkl

# For linux-arm64
Install-Package Sdcb.PaddleInference.runtime.linux-arm64

# For macOS-x64
Install-Package Sdcb.PaddleInference.runtime.osx-x64

# For macOS-arm64(m1/m2/m3/m4 based)
Install-Package Sdcb.PaddleInference.runtime.osx-arm64
```


### Detection Only
```csharp
// Install following packages:
// Sdcb.PaddleInference
// Sdcb.PaddleOCR
// Sdcb.PaddleOCR.Models.Local
// Sdcb.PaddleInference.runtime.win64.mkl
// OpenCvSharp4.runtime.win
byte[] sampleImageData;
string sampleImageUrl = @"https://www.tp-link.com.cn/content/images2017/gallery/4288_1920.jpg";
using (HttpClient http = new HttpClient())
{
    Console.WriteLine("Download sample image from: " + sampleImageUrl);
    sampleImageData = await http.GetByteArrayAsync(sampleImageUrl);
}

using (PaddleOcrDetector detector = new PaddleOcrDetector(LocalDetectionModel.ChineseV3, PaddleDevice.Mkldnn()))
using (Mat src = Cv2.ImDecode(sampleImageData, ImreadModes.Color))
{
    RotatedRect[] rects = detector.Run(src);
    using (Mat visualized = PaddleOcrDetector.Visualize(src, rects, Scalar.Red, thickness: 2))
    {
        string outputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "output.jpg");
        Console.WriteLine("OutputFile: " + outputFile);
        visualized.ImWrite(outputFile);
    }
}
```

### Table recognition
```csharp
// Install following packages:
// Sdcb.PaddleInference
// Sdcb.PaddleOCR
// Sdcb.PaddleOCR.Models.Local
// Sdcb.PaddleInference.runtime.win64.mkl
// OpenCvSharp4.runtime.win
using PaddleOcrTableRecognizer tableRec = new(LocalTableRecognitionModel.ChineseMobileV2_SLANET);
using Mat src = Cv2.ImRead(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "table.jpg"));
// Table detection
TableDetectionResult tableResult = tableRec.Run(src);

// Normal OCR
using PaddleOcrAll all = new(LocalFullModels.ChineseV3);
all.Detector.UnclipRatio = 1.2f;
PaddleOcrResult ocrResult = all.Run(src);

// Rebuild table
string html = tableResult.RebuildTable(ocrResult);
```

| Raw table                                                                                                      | Table model output                                                                                             | Rebuilt table                                                                                                  |
| -------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| ![table](https://user-images.githubusercontent.com/1317141/236091511-d8446cf6-dd75-4201-993a-3ec2f5999bca.jpg) | ![image](https://user-images.githubusercontent.com/1317141/236092184-78d7e035-ab28-465c-a7fe-6dc90aabc4c6.png) | ![image](https://user-images.githubusercontent.com/1317141/236091667-bbe28517-24a0-4f36-b559-f7026dd00ca4.png) |

## Technical details

There is 3 steps to do OCR:
1. Detection - Detect text's position, angle and area (`PaddleOCRDetector`)
2. Classification - Determin whether text should rotate 180 degreee.
3. Recognization - Recognize the area into text

## Optimize parameters and performance hints

### PaddleConfig.MkldnnCacheCapacity
Default value: `10`

This value has a positive correlation to the peak of memory usage that used by `mkldnn` and a negative correlation to the performance when providing different images.

To figure out each value corresponding to the peak memory usage, you should run the detection for various images(using the same image will not increase memory usage) continuously till the memory usage get stable within a variation of 1GB.

For more details please check the [pr #46](https://github.com/sdcb/PaddleSharp/pull/46) that decreases the default value and the [Paddle](https://github.com/PaddlePaddle/docs/blob/63362b7443c77a324f58a045bcc8d03bb59637fa/docs/design/mkldnn/caching/caching.md) document for `MkldnnCacheCapacity`.

### PaddleOcrAll.Enable180Classification
Default value: `false`

This directly effect the step 2, set to `false` can skip this step, which will unable to detect text from right to left(which should be acceptable because most text direction is from left to right).

Close this option can make the full process about  `~10%` faster.


### PaddleOcrAll.AllowRotateDetection
Default value: `true`

This allows detect any rotated texts. If your subject is 0 degree text (like scaned table or screenshot), you can set this parameter to `false`, which will improve OCR accurancy and little bit performance.


### PaddleOcrAll.Detector.MaxSize
Default value: `960`

This effect the the max size of step #1, lower this value can improve performance and reduce memory usage, but will also lower the accurancy.

You can also set this value to `null`, in that case, images will not scale-down to detect, performance will drop and memory will high, but should able to get better accurancy.


### How can I improve performance?
Please review the `Technical details` section and read the `Optimize parameters and performance hints` section, or UseGpu.

## FAQ
### How to integrate Sdcb.PaddleOCR to ASP.NET Core?

Please refer to this demo website, it contains a tutorial: [https://github.com/sdcb/paddlesharp-ocr-aspnetcore-demo](https://github.com/sdcb/paddlesharp-ocr-aspnetcore-demo)

In your service builder code, register a QueuedPaddleOcrAll Singleton:
```csharp
builder.Services.AddSingleton(s =>
{
    Action<PaddleConfig> device = builder.Configuration["PaddleDevice"] == "GPU" ? PaddleDevice.Gpu() : PaddleDevice.Mkldnn();
    return new QueuedPaddleOcrAll(() => new PaddleOcrAll(LocalFullModels.ChineseV3, device)
    {
        Enable180Classification = true,
        AllowRotateDetection = true,
    }, consumerCount: 1);
});
```

In your controller, use the registered `QueuedPaddleOcrAll` singleton:
```csharp
public class OcrController : Controller
{
    private readonly QueuedPaddleOcrAll _ocr;

    public OcrController(QueuedPaddleOcrAll ocr) { _ocr = ocr; }

    [Route("ocr")]
    public async Task<OcrResponse> Ocr(IFormFile file)
    {
        using MemoryStream ms = new();
        using Stream stream = file.OpenReadStream();
        stream.CopyTo(ms);
        using Mat src = Cv2.ImDecode(ms.ToArray(), ImreadModes.Color);
        double scale = 1;
        using Mat scaled = src.Resize(default, scale, scale);

        Stopwatch sw = Stopwatch.StartNew();
        string textResult = (await _ocr.Run(scaled)).Text;
        sw.Stop();

        return new OcrResponse(textResult, sw.ElapsedMilliseconds);
    }
}
```

### TensorRT 🚄

To use TensorRT, just specify `PaddleDevice.Gpu().And(PaddleDevice.TensorRt("shape-info.txt"))` instead of `PaddleDevice.Gpu()` to make it work. 💡

Please be aware, this shape info text file `**.txt` is bound to your model. **Different models have different shape info**, so if you're using a complex model like `Sdcb.PaddleOCR`, you should use different shapes for different models like this:
```csharp
using PaddleOcrAll all = new(model,
   PaddleDevice.Gpu().And(PaddleDevice.TensorRt("det.txt")),
   PaddleDevice.Gpu().And(PaddleDevice.TensorRt("cls.txt")),
   PaddleDevice.Gpu().And(PaddleDevice.TensorRt("rec.txt")))
{
   Enable180Classification = true,
   AllowRotateDetection = true,
};
```

In this case:
* `DetectionModel` will use `det.txt` 🔍
* `180DegreeClassificationModel` will use `cls.txt` 🔃
* `RecognitionModel` will use `rec.txt` 🔡

**NOTE 📝:**

The first round of `TensorRT` running will generate a shape info `**.txt` file in this folder: `%AppData%\Sdcb.PaddleInference\TensorRtCache`. It will take around 100 seconds to finish TensorRT cache generation. After that, it should be faster than the general `GPU`. 🚀

In this case, if something strange happens (for example, you mistakenly create the same `shape-info.txt` file for different models), you can delete this folder to generate TensorRT cache again: `%AppData%\Sdcb.PaddleInference\TensorRtCache`. 🗑️