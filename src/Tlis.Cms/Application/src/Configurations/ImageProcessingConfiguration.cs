namespace Tlis.Cms.Application.Configurations;

internal sealed class ImageProcessingConfiguration
{
    public required ImageFormatConfiguration User { get; set; }

    public required ImageFormatConfiguration Show { get; set; }

    public required ImageFormatConfiguration Broadcast { get; set; }
}