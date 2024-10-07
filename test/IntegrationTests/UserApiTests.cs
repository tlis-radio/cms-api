using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bogus;
using Tlis.Cms.Api;
using Tlis.Cms.Application.Contracts.Api.Requests.Users;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Contracts.Api.Responses.UserGetResponses;
using Tlis.Cms.Domain.Constants;

namespace Tlis.Cms.Test.IntegrationTests;

public class UserApiTests(ApiWebApplicationFactory<Program> factory) : IClassFixture<ApiWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    [Fact]
    public async Task Create_user_update_profile_image_and_delete_user_all_responses_should_be_created()
    {
        // Arrange
        var userCreateRequestGenerator = new Faker<UserCreateRequest>()
            .StrictMode(true)
            .RuleFor(x => x.Firstname, f => f.Person.FirstName)
            .RuleFor(x => x.Lastname, f => f.Person.LastName)
            .RuleFor(x => x.Nickname, f => f.Person.UserName)
            .RuleFor(x => x.PreferNicknameOverName, f => f.Random.Bool())
            .RuleFor(x => x.CmsAdminAccess, f => false)
            .RuleFor(x => x.Abouth, f => f.Lorem.Sentence())
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.RoleHistory, f => [])
            .RuleFor(x => x.MembershipHistory, f => []);

        var userCreateRequest = userCreateRequestGenerator.Generate();

        using var imageStream = File.OpenRead("./Assets/image.png");
        var userUpdateProfileImageRequestGenerator = new MultipartFormDataContent
        {
            { new StreamContent(imageStream), "profileImage", "profileImage.jpg" }
        };

        // Act

        var createResponse = await _client.PostAsJsonAsync("user", userCreateRequest);

        var createContent = await createResponse.Content.ReadFromJsonAsync<BaseCreateResponse>();

        var updateProfileImageResponse = await _client.PutAsync($"user/{createContent!.Id}/profile-image", userUpdateProfileImageRequestGenerator);

        var deleteResponse = await _client.DeleteAsync($"user/{createContent!.Id}");

        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createContent.Should().NotBeNull();
        createContent!.Id.Should().NotBeEmpty();

        updateProfileImageResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task CRUD_user_without_admin_access_and_all_responses_should_be_created()
    {
        // Arrange
        var userMembershipHistoryCreateRequestGenerator = new Faker<UserMembershipHistoryCreateRequest>()
            .StrictMode(true)
            .RuleFor(x => x.MembershipId, f => MembershipStatusId.Active)
            .RuleFor(x => x.ChangeDate, f => f.Date.Past().ToUniversalTime())
            .RuleFor(x => x.Description, f => f.Lorem.Sentence());

        var userUpdateRequestMembershipHistoryGenerator = new Faker<UserUpdateRequestMembershipHistory>()
            .StrictMode(true)
            .RuleFor(x => x.Id, f => null)
            .RuleFor(x => x.MembershipId, f => MembershipStatusId.Archive)
            .RuleFor(x => x.ChangeDate, f => f.Date.Past().ToUniversalTime())
            .RuleFor(x => x.Description, f => f.Lorem.Sentence());

        var userCreateRequestGenerator = new Faker<UserCreateRequest>()
            .StrictMode(true)
            .RuleFor(x => x.Firstname, f => f.Person.FirstName)
            .RuleFor(x => x.Lastname, f => f.Person.LastName)
            .RuleFor(x => x.Nickname, f => f.Person.UserName)
            .RuleFor(x => x.PreferNicknameOverName, f => f.Random.Bool())
            .RuleFor(x => x.CmsAdminAccess, f => false)
            .RuleFor(x => x.Abouth, f => f.Lorem.Sentence())
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.RoleHistory, f => [])
            .RuleFor(x => x.MembershipHistory, f => userMembershipHistoryCreateRequestGenerator.Generate(1));

        var userCreateRequest = userCreateRequestGenerator.Generate();

        var userUpdateRequestGenerator = new Faker<UserUpdateRequest>()
            .RuleFor(x => x.Firstname, f => f.Person.FirstName)
            .RuleFor(x => x.Lastname, f => f.Person.LastName)
            .RuleFor(x => x.Nickname, f => f.Person.UserName)
            .RuleFor(x => x.PreferNicknameOverName, f => f.Random.Bool())
            .RuleFor(x => x.CmsAdminAccess, f => false)
            .RuleFor(x => x.Abouth, f => f.Lorem.Sentence())
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.RoleHistory, f => [])
            .RuleFor(x => x.MembershipHistory, f => userUpdateRequestMembershipHistoryGenerator.Generate(1));

        var userUpdateRequestRequest = userUpdateRequestGenerator.Generate();

        // Act
        var createResponse = await _client.PostAsJsonAsync("user", userCreateRequest);

        var createContent = await createResponse.Content.ReadFromJsonAsync<BaseCreateResponse>();

        var updateResponse = await _client.PutAsJsonAsync($"user/{createContent!.Id}", userUpdateRequestRequest);

        var getResponse = await _client.GetFromJsonAsync<UserGetResponse>($"user/{createContent!.Id}", _jsonSerializerOptions);

        var deleteResponse = await _client.DeleteAsync($"user/{createContent!.Id}");

        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createContent.Should().NotBeNull();
        createContent!.Id.Should().NotBeEmpty();

        updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        getResponse.Should().NotBeNull();
        getResponse!.Firstname.Should().Be(userUpdateRequestRequest.Firstname);
        getResponse!.Lastname.Should().Be(userUpdateRequestRequest.Lastname);
        getResponse!.Nickname.Should().Be(userUpdateRequestRequest.Nickname);
        getResponse!.PreferNicknameOverName.Should().Be(userUpdateRequestRequest.PreferNicknameOverName);
        getResponse!.CmsAdminAccess.Should().Be(userUpdateRequestRequest.CmsAdminAccess);
        getResponse!.Abouth.Should().Be(userUpdateRequestRequest.Abouth);
        getResponse!.Email.Should().Be(userUpdateRequestRequest.Email);
        getResponse!.MembershipHistory.Should().NotBeEmpty();
        getResponse!.MembershipHistory.Should().HaveCount(1);
        getResponse!.MembershipHistory.First().Membership.Id.Should().Be(userUpdateRequestRequest.MembershipHistory.First().MembershipId);
        // getResponse!.MembershipHistory.First().ChangeDate.Should().Be(userUpdateRequestRequest.MembershipHistory.First().ChangeDate);
        getResponse!.MembershipHistory.First().Description.Should().Be(userUpdateRequestRequest.MembershipHistory.First().Description);

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}