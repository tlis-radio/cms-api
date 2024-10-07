using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tlis.Cms.Application.Configurations;
using Tlis.Cms.Domain.Constants;
using Tlis.Cms.Domain.Entities.Images;

namespace Tlis.Cms.Application.Services.Interfaces;

internal interface IImageProcessingService
{
    Task<Image> ProcessImageAsync(IFormFile image, ImageType imageType, ImageFormatConfiguration format);
}