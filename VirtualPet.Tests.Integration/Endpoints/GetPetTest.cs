using System.Net;

namespace VirtualPet.Tests.Integration.Endpoints;

public class GetPetTest(WebApplicationTestFactory<Program> factory) : IClassFixture<WebApplicationTestFactory<Program>>
{
    [Fact]
    public async Task GetPet_ReturnsOkResponse()
    {
        // Arrange
        var client = factory.CreateClient();
        var ownerId = Guid.NewGuid();

        // Act
        var response = await client.GetAsync($"/api/pets");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}