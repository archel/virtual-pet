using System.Net;
using System.Net.Http.Json;

using VirtualPet.Domain.Pet;

namespace VirtualPet.Tests.Integration.Endpoints;

public class GetPetTest(WebApplicationTestFactory<Program> factory) : IClassFixture<WebApplicationTestFactory<Program>>
{
    [Fact]
    public async Task GetPet_ReturnsOkResponse()
    {
        // Arrange
        var client = factory.CreateClient();
        var ownerId = Guid.NewGuid();
        var expectedPet = new PetDto(
            Guid.NewGuid(),
            ownerId,
            "Fluffy",
            0,
            100,
            100,
            100,
            100
        );
        // Act
        var response = await client.GetAsync($"/api/pets");
        var petResponse = await response.Content.ReadFromJsonAsync<PetDto>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(expectedPet, petResponse);        
    }
}