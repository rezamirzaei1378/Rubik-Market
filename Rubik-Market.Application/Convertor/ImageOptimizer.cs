using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Rubik_Market.Application.Convertor;

public class ImageOptimizer
{

    public void ImageResizer(string inputImagePath, string outputImagePath, int? width, int? height)
    {
        var costumeWidth = width ?? 100;
        var costumeHeight = height ?? 100;

        using (var image = Image.Load(inputImagePath))
        {
            image.Mutate(x => x.Resize(costumeWidth, costumeHeight));

            image.Save(outputImagePath, new JpegEncoder()
            {
                Quality = 100
            });
        }
    }
}