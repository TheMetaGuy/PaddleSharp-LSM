using Sdcb.PaddleOCR.Models.Online;
using Xunit;
using Xunit.Abstractions;

namespace Sdcb.PaddleOCR.Tests;

public class DownloadCheckTest
{
#pragma warning disable IDE0052 // ɾ��δ����˽�г�Ա
    private readonly ITestOutputHelper _console;
#pragma warning restore IDE0052 // ɾ��δ����˽�г�Ա

    public DownloadCheckTest(ITestOutputHelper console)
    {
        _console = console;
    }

    [Fact]
    public async Task DownloadCheck()
    {
        await LocalDictOnlineRecognizationModel.ChineseMobileV2.DownloadAsync();
    }
}