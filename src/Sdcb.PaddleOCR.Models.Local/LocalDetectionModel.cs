using Sdcb.PaddleInference;
using Sdcb.PaddleOCR.Models.Local.Details;

namespace Sdcb.PaddleOCR.Models.Local;

/// <summary>
/// This class represents a local detection model used by PaddleOCR to detect text from an image.
/// </summary>
public class LocalDetectionModel : DetectionModel
{
    /// <summary>
    /// Gets the name of this model.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalDetectionModel"/> class with the specified name and version.
    /// </summary>
    /// <param name="name">The name of the model.</param>
    /// <param name="version">The version of the model.</param>
    public LocalDetectionModel(string name, ModelVersion version) : base(version)
    {
        Name = name;
    }

    /// <inheritdoc/>
    public override PaddleConfig CreateConfig() => Utils.LocalModel(Name, Version);

    /// <summary>
    /// Gets the Chinese language detection model for version 5.
    /// </summary>
    public static LocalDetectionModel ChineseV5 => new("mobile-zh-det", ModelVersion.V5);

    /// <summary>
    /// Gets the English language detection model for version 5.
    /// </summary>
    public static LocalDetectionModel EnglishV5 => new("mobile-en-det", ModelVersion.V5);

    /// <summary>
    /// Chinese server inference detection model for version 5. Supports Chinese, English, and multilingual text detection.
    /// It's much larger ( approx 90 MB) but more accurate than the mobile version models 
    /// </summary>
    public static LocalDetectionModel ChineseServerV5 => new("server-zh-det", ModelVersion.V5);

    /// <summary>
    /// Chinese detection v4 model used by PaddleOCR to detect text from an image, supporting multiple languages(Size: 4.6M).
    /// </summary>
    public static LocalDetectionModel ChineseV4 => new("ch_PP-OCRv4_det", ModelVersion.V4);

    /// <summary>
    /// Original lightweight model, supporting Chinese, English, multilingual text detection(Size: 3.8M).
    /// </summary>
    public static LocalDetectionModel ChineseV3 => new("ch_PP-OCRv3_det", ModelVersion.V3);

    /// <summary>
    /// Original lightweight detection model, supporting English(Size: 3.8M).
    /// </summary>
    public static LocalDetectionModel EnglishV3 => new("en_PP-OCRv3_det", ModelVersion.V3);

    /// <summary>
    /// Original lightweight detection model, supporting multiple languages(Size: 3.8M).
    /// </summary>
    public static LocalDetectionModel MultiLanguageV3 => new("ml_PP-OCRv3_det", ModelVersion.V3);

    /// <summary>
    /// Gets an array of all the available <see cref="LocalDetectionModel"/> objects.
    /// </summary>
    public static LocalDetectionModel[] All => new[]
    {
        ChineseV5,
        ChineseServerV5,
        EnglishV5,
        ChineseV4,
        ChineseV3,
        EnglishV3,
        MultiLanguageV3,
    };
}
