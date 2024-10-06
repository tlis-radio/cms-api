using System.Net;
using System.Net.Http.Json;
using Tlis.Cms.Api;
using Tlis.Cms.Application.Contracts.Api.Responses.RoleGetAllResponses;

namespace Tlis.Cms.Test.IntegrationTests;

public class RoleApiTests(ApiWebApplicationFactory<Program> factory) : IClassFixture<ApiWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Get_all_roles_and_response_should_not_be_empty()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("role");

        var content = await response.Content.ReadFromJsonAsync<RoleGetAllResponse>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().NotBeNull();
        content!.Results.Should().NotBeEmpty();
    }
}