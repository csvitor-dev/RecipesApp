using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Services;

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
}