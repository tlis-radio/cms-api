using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Auth0.Core.Exceptions;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.Extensions.Options;
using RandomString4Net;
using Tlis.Cms.Infrastructure.Configurations;
using Tlis.Cms.Infrastructure.Exceptions;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Infrastructure.Services;

internal sealed class AuthProviderManagementService(
    IAuthProviderTokenService tokenProviderService,
    HttpClient httpClient,
    IOptions<AuthProviderConfiguration> configuration)
    : IAuthProviderManagementService
{
    private readonly IManagementConnection _managementConnection = new HttpClientManagementConnection(httpClient);

    private readonly string _domain = configuration.Value.Domain;

    public async ValueTask<string> CreateUserAsync(string email, string[] roleIds)
    {
        try
        {
            using var client = await GetApiClientAsync();

            var response = await client.Users.CreateAsync(
                new UserCreateRequest
                {
                    Email = email,
                    Password = RandomString.GetString(Types.ALPHABET_MIXEDCASE_WITH_SYMBOLS, 15),
                    Connection = "Username-Password-Authentication"
                }
            );
            
            if (roleIds.Length > 0)
            {
                await client.Users.AssignRolesAsync(
                    response.UserId,
                    new AssignRolesRequest { Roles = roleIds });
            }

            return response.UserId;
        }
        catch (ErrorApiException ex)
        {
            throw ex.StatusCode switch
            {
                HttpStatusCode.Conflict => new AuthProviderUserAlreadyExistsException(ex.Message),
                HttpStatusCode.BadRequest => new AuthProviderBadRequestException(ex.Message),
                _ => new AuthProviderException(ex.Message)
            };
        }
    }

    public async ValueTask UpdateUserRolesAsync(string id, string[] roleIds)
    {
        using var client = await GetApiClientAsync();

        await client.Users.AssignRolesAsync(
            id,
            new AssignRolesRequest { Roles = roleIds });
    }

    public async Task DeleteUserAsync(string id)
    {
        using var client = await GetApiClientAsync();

        await client.Users.DeleteAsync(id);
    }

    public async Task<List<Role>> GetAllRolesAsync()
    {
        using var client = await GetApiClientAsync();

        var response = await client.Roles.GetAllAsync(new GetRolesRequest());

        return [.. response];
    }

    private async ValueTask<IManagementApiClient> GetApiClientAsync() =>
        new ManagementApiClient(await tokenProviderService.GetAccessTokenAsync(), _domain, _managementConnection);
}