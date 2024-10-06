using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tlis.Cms.Application.Configurations;
using Tlis.Cms.Domain.Entities.Images;

namespace Tlis.Cms.Application.Services.Interfaces;

internal interface IImageProcessingService
{
    Task<Image> CreateImageAsync(IFormFile image, ImageFormatConfiguration format);
}