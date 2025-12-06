using System.Net;
using System.Text;
using System.Text.Json;

using Microsoft.Extensions.DependencyInjection;

using VirtualPet.Domain.Pet;
using VirtualPet.Presentation.Requests;

namespace VirtualPet.Tests.Integration.Endpoints;

public class CreatePetTest(WebApplicationTestFactory<Program> factory): IClassFixture<WebApplicationTestFactory<Program>>
{
    private const string A_PET_NAME = "Fluffy";
    private readonly IPetRepository _petRepository = factory.Services.GetRequiredService<IPetRepository>();
    [Fact]
    public async Task Creates_Pet()
    {
        // Arrange
        var client = factory.CreateClient();
        var ownerId = Guid.NewGuid();
        var request = new CreatePetRequest(ownerId, A_PET_NAME);

        // Act
        var response = await client.PostAsync("/api/pets", new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var pet = (await _petRepository.GetByOwnerIdAsync(ownerId, CancellationToken.None))?.ToDto();
        Assert.Equal(A_PET_NAME, pet?.Name);
        Assert.Equal(ownerId, pet?.OwnerId);
    }
}