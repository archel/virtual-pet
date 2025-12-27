using System.Net;
using System.Text;
using System.Text.Json;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;

using VirtualPet.Domain.Pet;
using VirtualPet.Presentation.Requests;
using VirtualPet.Tests.Integration.Mocks;

namespace VirtualPet.Tests.Integration.Endpoints;

public class CreatePetTest(WebApplicationTestFactory<Program> factory): IClassFixture<WebApplicationTestFactory<Program>>
{
    private const string A_PET_NAME = "Fluffy";
    private readonly IPetRepository _petRepository = factory.Services.GetRequiredService<IPetRepository>();

    private readonly FakeGuidGenerator _guidGenerator = factory.Services.GetRequiredService<FakeGuidGenerator>();

    [Fact]
    public async Task Creates_Pet()
    {
        // Arrange
        var client = factory.CreateClient();
        var ownerId = Guid.NewGuid();
        var request = new CreatePetRequest(ownerId, A_PET_NAME);
        var petGuid = Guid.NewGuid();
        _guidGenerator.EnqueueGuid(petGuid);

        // Act
        var response = await client.PostAsync("/api/pets", new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        // Assert
        HttpStatusCode.Created.Should().Be(response.StatusCode);
        var pet = await _petRepository.GetByOwnerIdAsync(ownerId, CancellationToken.None);
        var expectedPet = Pet.Create(petGuid, ownerId, A_PET_NAME);
        pet.Should().BeEquivalentTo(expectedPet);
    }
}