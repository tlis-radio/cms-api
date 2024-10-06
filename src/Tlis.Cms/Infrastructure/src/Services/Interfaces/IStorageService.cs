using System;
using System.IO;
using System.Threading.Tasks;

namespace Tlis.Cms.Infrastructure.Services.Interfaces;

public interface IStorageService
{
    public Task<bool> DeleteImage(string fileUrl);

    public Task<string> UploadImage(Stream stream, Guid imageId);

    Task UpdateImage(Stream stream, Guid imageId);
}