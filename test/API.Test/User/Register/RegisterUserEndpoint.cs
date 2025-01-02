using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace API.Test.User.Register;

public class RegisterUserEndpoint(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    
    [Fact]
    public async Task Test_OnSuccess()
    {
        var request = RegisterUserRequestJSONMockFactory.CreateMock();

        var response = await _client.PostAsJsonAsync("/Users", request);
        await using var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        result.RootElement.GetProperty("name").GetString().Should()
            .NotBeNullOrWhiteSpace().And.Be(request.Name);
    }
}