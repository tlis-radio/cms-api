using System;
using System.IO;
using System.Threading.Tasks;

namespace Tlis.Cms.Infrastructure.Services.Interfaces;

public interface ICloudeStorageService
{
    string? GetUserImageUrl(string? fileName);

    string? GetShowImageUrl(string? fileName);

    string? GetBroadcastImageUrl(string? fileName);

    Task<bool> DeleteUserImageAsync(string fileUrl);

    Task<string> UploadUserImageAsync(Stream stream, Guid imageId);

    Task<bool> DeleteShowImageAsync(string fileUrl);

    Task<string> UploadShowImageAsync(Stream stream, Guid imageId);

    Task<bool> DeleteBroadcastImageAsync(string fileUrl);

    Task<string> UploadBroadcastImageAsync(Stream stream, Guid imageId);
}