using Newtonsoft.Json;

namespace DCIAspNetTemplate.FunctionalTests;

public class FunctionalTests
{
    private readonly HttpClient _client;

    // Create an HttpClient instance pointing to your actual API (running somewhere, e.g., localhost:5000)
    public FunctionalTests()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8080") // Replace with the actual URL of your running API
        };
    }

    [Fact]
    public async Task TestGetEndpoint_ReturnsOk()
    {
        // Arrange: Define the URL endpoint you want to test
        var url = "/api/values"; // Replace with your actual API endpoint

        // Act: Send the GET request to the API
        var response = await _client.GetAsync(url);

        // Assert: Check if the response status code is 200 OK
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        // Optionally, you can assert the response content as well
        Assert.Contains("expected_value", responseBody);
    }

    [Fact]
    public async Task TestPostEndpoint_ReturnsCreated()
    {
        // Arrange: Prepare your request data
        var requestData = new { Name = "Test Name", Description = "Test Description" };
        var content = new StringContent(JsonConvert.SerializeObject(requestData), System.Text.Encoding.UTF8, "application/json");

        var url = "/api/items"; // Replace with your actual API endpoint

        // Act: Send the POST request
        var response = await _client.PostAsync(url, content);

        // Assert: Check if the response status code is 201 Created
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        // Optionally, you can assert that the returned content matches what you expect
        Assert.Contains("Test Name", responseBody);
    }
}
