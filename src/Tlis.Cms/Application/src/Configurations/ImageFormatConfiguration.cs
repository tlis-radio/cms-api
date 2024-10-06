using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.Application.Configurations;

internal sealed class ImageFormatConfiguration
{
    [Required]
    public required int Height { get; set; } = 150;

    [Required]
    public required int Width { get; set; } = 150;
}