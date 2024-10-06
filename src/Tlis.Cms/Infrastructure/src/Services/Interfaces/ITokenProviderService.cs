using System.Threading.Tasks;

namespace Tlis.Cms.Infrastructure.Services.Interfaces;

public interface ITokenProviderService
{
    ValueTask<string> GetAuth0AccessToken(bool force = false);
}