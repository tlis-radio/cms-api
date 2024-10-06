using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.Infrastructure.Configurations;

internal sealed class Auth0Configuration
{
    [Required]
    public required string Domain { get; set; }

    [Required]
    public required string ClientId { get; set; }

    [Required]
    public required string ClientSecret { get; set; }
}