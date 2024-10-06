using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.Infrastructure.Configurations;

internal sealed class ServiceUrlsConfiguration
{
    [Required]
    public required string StorageAccount { get; set; }
}