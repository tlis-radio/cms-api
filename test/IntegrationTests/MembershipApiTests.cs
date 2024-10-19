using System.Net;
using System.Net.Http.Json;
using Tlis.Cms.Api;
using Tlis.Cms.Application.Contracts.Api.Responses.MembershipStatusGetAllResponses;

namespace Tlis.Cms.Test.IntegrationTests;

public class MembershipApiTests(ApiWebApplicationFactory<Program> factory) : IClassFixture<ApiWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Get_all_memberships_and_response_should_not_be_empty()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("membership");

        var content = await response.Content.ReadFromJsonAsync<MembershipStatusGetAllResponse>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().NotBeNull();
        content!.Results.Should().NotBeEmpty();
    }
}