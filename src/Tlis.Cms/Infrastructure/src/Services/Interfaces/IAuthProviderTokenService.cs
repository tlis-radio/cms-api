using System.Threading.Tasks;

namespace Tlis.Cms.Infrastructure.Services.Interfaces;

public interface IAuthProviderTokenService
{
    ValueTask<string> GetAccessTokenAsync(bool force = false);
}