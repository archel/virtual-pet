using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;

using VirtualPet.Domain.Pet;
using VirtualPet.Tests.Integration.Mocks;

namespace VirtualPet.Tests.Integration.Endpoints;

public class GetPetTest(WebApplicationTestFactory<Program> factory) : IClassFixture<WebApplicationTestFactory<Program>>
{
    private readonly IPetRepository _petRepository = factory.Services.GetRequiredService<IPetRepository>();
    private readonly FakeGuidGenerator _guidGenerator = factory.Services.GetRequiredService<FakeGuidGenerator>();
    
    [Fact]
    public async Task GetPet_ReturnsOkResponse()
    {
        // Arrange
        var client = factory.CreateClient();
        var ownerId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        _guidGenerator.EnqueueGuid(petId);
        var pet = Pet.Create(petId, ownerId, "Fluffy");
        await _petRepository.AddAsync(pet, CancellationToken.None);
        var expectedPet = new PetDto(
            petId,
            ownerId,
            "Fluffy",
            0,
            50,
            50,
            50,
            100
        );
        // Act
        var response = await client.GetAsync($"/api/pets/{ownerId}");

        // Assert
        HttpStatusCode.OK.Should().Be(response.StatusCode);
        var petResponse = await response.Content.ReadFromJsonAsync<PetDto>();
        petResponse.Should().BeEquivalentTo(expectedPet);
    }
}