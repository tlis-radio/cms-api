using System;
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.Infrastructure.Configurations;

internal sealed class CloudStorageConfiguration
{
    [Required]
    public required CloudStorageConfigurationCdn Cdn { get; set; }

    [Required]
    public required CloudStorageConfigurationAuthentication Authentication { get; set; }
}

internal sealed class CloudStorageConfigurationAuthentication
{
    [Required]
    public required string ConnectionString { get; set; }
}

internal sealed class CloudStorageConfigurationCdn
{
    [Required]
    public required string Url { get; set; }

    [Required]
    public required CloudStorageConfigurationCdnFolders Folders { get; set; }
}

internal sealed class CloudStorageConfigurationCdnFolders
{
    [Required]
    public required string UserImages { get; set; }

    [Required]
    public required string ShowImages { get; set; }

    [Required]
    public required string BroadcastImages { get; set; }
}