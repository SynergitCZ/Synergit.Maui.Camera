using ZXing;

namespace Synergit.Maui.Camera.ZXingHelper;

public record BarcodeEventArgs
{
    public Result[] Result { get; init; }
}
