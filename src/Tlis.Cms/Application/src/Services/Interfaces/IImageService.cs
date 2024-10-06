using System.Drawing;
using System.IO;
using NetVips;

namespace Tlis.Cms.Application.Services.Interfaces;

internal interface IImageService
{
    Stream ToWebp(Image image);

    Image ToImage(Stream stream);

    Image Resize(Image image, int width, int height);

    Image Crop(Image image, int left, int top, int width, int height);

    Size GetSize(Image image);
}