using System;
using System.Net.Http;
using System.Threading.Tasks;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Tlis.Cms.Infrastructure.Configurations;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Infrastructure.Services;

internal sealed class AuthProviderTokenService(
    HttpClient httpClient,
    IOptions<AuthProviderConfiguration> configuration,
    IMemoryCache memoryCache) : IAuthProviderTokenService
{
    private readonly IAuthenticationConnection _connection = new HttpClientAuthenticationConnection(httpClient);

    private readonly AuthProviderConfiguration _configuration = configuration.Value;

    private const string Auth0AccessTokenKey = "AUTH0_ACCESS_TOKEN_KEY";

    public async ValueTask<string> GetAccessTokenAsync(bool force = false)
    {
        if (force is false && memoryCache.TryGetValue<string>(Auth0AccessTokenKey, out var accessToken))
        {
            if (accessToken is not null)
                return accessToken;
        }

        var client = new AuthenticationApiClient(_configuration.Domain, _connection);
        var response = await client.GetTokenAsync(new ClientCredentialsTokenRequest
        {
            Audience = $"https://{_configuration.Domain}/api/v2/",
            ClientId = _configuration.ClientId,
            ClientSecret = _configuration.ClientSecret
        });

        memoryCache.Set(
            Auth0AccessTokenKey,
            response.AccessToken,
            TimeSpan.FromSeconds(response.ExpiresIn));

        return response.AccessToken;
    }
}