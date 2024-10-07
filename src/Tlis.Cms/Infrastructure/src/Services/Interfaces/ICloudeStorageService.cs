using System;
using System.IO;
using System.Threading.Tasks;

namespace Tlis.Cms.Infrastructure.Services.Interfaces;

public interface ICloudeStorageService
{
    Task<bool> DeleteUserImage(string fileUrl);

    Task<string> UploadUserImage(Stream stream, Guid imageId);

    Task<bool> DeleteShowImage(string fileUrl);

    Task<string> UploadShowImage(Stream stream, Guid imageId);

    Task<bool> DeleteBroadcastImage(string fileUrl);

    Task<string> UploadBroadcastImage(Stream stream, Guid imageId);
}