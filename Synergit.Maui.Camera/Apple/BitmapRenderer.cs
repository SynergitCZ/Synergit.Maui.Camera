#if IOS || MACCATALYST
using CoreGraphics;
using UIKit;
using ZXing.Common;
using ZXing.Rendering;
using ZXing;

namespace Synergit.Maui.Camera.Platforms.Apple;

public class BitmapRenderer : IBarcodeRenderer<UIImage>
{
    public CGColor Foreground { get; set; }
    public CGColor Background { get; set; }

    public BitmapRenderer()
    {
        Foreground = new CGColor(0f, 0f, 0f);
        Background = new CGColor(1.0f, 1.0f, 1.0f);
    }
    public UIImage Render(BitMatrix matrix, BarcodeFormat format, string content)
    {
        return Render(matrix, format, content, new EncodingOptions());
    }


    /* JH - Using new renderer to awoid unsuported functions */
    public UIImage Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
    {
        var renderFormat = UIGraphicsImageRendererFormat.DefaultFormat;
        renderFormat.Opaque = true;
        var render = new UIGraphicsImageRenderer(new CGSize(matrix.Width, matrix.Height), renderFormat);

        var image = render.CreateImage((UIGraphicsImageRendererContext context) =>
        {
            for (int x = 0; x < matrix.Width; x++)
            {
                for (int y = 0; y < matrix.Height; y++)
                {
                    context.CGContext.SetFillColor(matrix[x, y] ? Foreground : Background);
                    context.FillRect(new CGRect(x, y, 1, 1));
                }
            }
        }
        );

        return image;
    }

}
#endif
