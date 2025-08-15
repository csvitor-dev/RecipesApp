using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using API.Test.InlineData;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Services;
using RecipesApp.Exception.Resources;

namespace API.Test.User.Register;

public class RegisterUserEndpoint(WebApplicationMockFactory factory)
    : IClassFixture<WebApplicationMockFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Test_OnSuccess()
    {
        var request = RegisterUserRequestJSONMockFactory.CreateMock();

        var response = await _client.PostAsJsonAsync("/Users", request);
        await using var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        var username = result.RootElement.GetProperty("name").GetString();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.False(string.IsNullOrWhiteSpace(username));
        Assert.Equal(request.Name, username);
    }

    [Theory]
    [ClassData(typeof(CultureInlineData))]
    public async Task Test_EmptyName_OnFailure(string? culture)
    {
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", culture);
        var request = RegisterUserRequestJSONMockFactory.CreateMockWithoutName();

        var response = await _client.PostAsJsonAsync("/Users", request);
        await using var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        var errors = result.RootElement.GetProperty("errors")
            .EnumerateArray().ToList();

        var expectedMessage = ResourcesAccessor.GetMessageByCulture("NAME_REQUIRED", culture);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Single(errors,
            error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineData))]
    public async Task Test_EmptyEmail_OnFailure(string? culture)
    {
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", culture);
        var request = RegisterUserRequestJSONMockFactory.CreateMockWithoutEmail();

        var response = await _client.PostAsJsonAsync("/Users", request);
        await using var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        var errors = result.RootElement.GetProperty("errors")
            .EnumerateArray().ToList();

        var expectedMessage = ResourcesAccessor.GetMessageByCulture("EMAIL_REQUIRED", culture);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Single(errors,
            error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineData))]
    public async Task Test_InvalidEmail_OnFailure(string? culture)
    {
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", culture);
        var request = RegisterUserRequestJSONMockFactory.CreateMockWithInvalidEmail("email.com");

        var response = await _client.PostAsJsonAsync("/Users", request);
        await using var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        var errors = result.RootElement.GetProperty("errors")
            .EnumerateArray().ToList();

        var expectedMessage = ResourcesAccessor.GetMessageByCulture("EMAIL_INVALID", culture);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Single(errors,
            error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineData))]
    public async Task Test_EmptyPassword_OnFailure(string? culture)
    {
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", culture);
        var request = RegisterUserRequestJSONMockFactory.CreateMockWithoutPassword();

        var response = await _client.PostAsJsonAsync("/Users", request);
        await using var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        var errors = result.RootElement.GetProperty("errors")
            .EnumerateArray().ToList();

        var expectedMessage = ResourcesAccessor.GetMessageByCulture("PASSWORD_REQUIRED", culture);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Single(errors,
            error => error.GetString()!.Equals(expectedMessage));
    }
}