using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bogus;
using Tlis.Cms.Api;
using Tlis.Cms.Application.Contracts.Api.Requests.Shows;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Contracts.Api.Responses.ShowDetailsGetResponses;
using Tlis.Cms.Domain.Constants;

namespace Tlis.Cms.Test.IntegrationTests;

public class ShowApiTests(ApiWebApplicationFactory<Program> factory) : IClassFixture<ApiWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    [Fact]
    public async Task Create_show_update_profile_image_and_delete_show_all_responses_should_be_created()
    {
        // Arrange
        var showCreateRequestGenerator = new Faker<ShowCreateRequest>()
            .StrictMode(true)
            .RuleFor(x => x.Name, f => f.Random.Word())
            .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
            .RuleFor(x => x.ModeratorIds, f => []);

        var showCreateRequest = showCreateRequestGenerator.Generate();

        using var imageStream = File.OpenRead("./Assets/image.png");
        var showUpdateProfileImageRequest = new MultipartFormDataContent
        {
            { new StreamContent(imageStream), "profileImage", "profileImage.jpg" }
        };

        // Act

        var createResponse = await _client.PostAsJsonAsync("show", showCreateRequest);

        var createContent = await createResponse.Content.ReadFromJsonAsync<BaseCreateResponse>();

        var updateProfileImageResponse = await _client.PutAsync($"show/{createContent!.Id}/profile-image", showUpdateProfileImageRequest);

        var deleteResponse = await _client.DeleteAsync($"show/{createContent!.Id}");

        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createContent.Should().NotBeNull();
        createContent!.Id.Should().NotBeEmpty();

        updateProfileImageResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task CRUD_show_and_all_responses_should_be_created()
    {
        // Arrange
        var showCreateRequestGenerator = new Faker<ShowCreateRequest>()
            .StrictMode(true)
            .RuleFor(x => x.Name, f => f.Random.Word())
            .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
            .RuleFor(x => x.ModeratorIds, f => []);

        var showCreateRequest = showCreateRequestGenerator.Generate();

        var showUpdateRequestGenerator = new Faker<ShowUpdateRequest>()
            .RuleFor(x => x.Name, f => f.Random.Word())
            .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
            .RuleFor(x => x.ModeratorIds, f => []);

        var showUpdateRequestRequest = showUpdateRequestGenerator.Generate();

        // Act
        var createResponse = await _client.PostAsJsonAsync("show", showCreateRequest);

        var createContent = await createResponse.Content.ReadFromJsonAsync<BaseCreateResponse>();

        var updateResponse = await _client.PutAsJsonAsync($"show/{createContent!.Id}", showUpdateRequestRequest);

        var getResponse = await _client.GetFromJsonAsync<ShowDetailsGetResponse>($"show/{createContent!.Id}", _jsonSerializerOptions);

        var deleteResponse = await _client.DeleteAsync($"show/{createContent!.Id}");

        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createContent.Should().NotBeNull();
        createContent!.Id.Should().NotBeEmpty();

        updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        getResponse.Should().NotBeNull();
        getResponse!.Name.Should().Be(showUpdateRequestRequest.Name);
        getResponse!.Description.Should().Be(showUpdateRequestRequest.Description);
        // getResponse!.Moderators.First().Id.Should().Be(showUpdateRequestRequest.ModeratorIds.First());

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}