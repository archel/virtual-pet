using FluentAssertions;

using NSubstitute;

using VirtualPet.Application.Queries;
using VirtualPet.Domain.Pet;

namespace VirtualPet.Tests.Unit.Application.Queries;

public class GetPetTest
{
    private readonly IPetRepository _petRepository;
    private readonly GetPetQueryHandler _handler;

    public GetPetTest()
    {
        _petRepository = Substitute.For<IPetRepository>();
        _handler = new GetPetQueryHandler(_petRepository);
    }

    [Fact]
    public async Task ShouldReturnOwnersPetDetails()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var petId = Guid.NewGuid();
        var petName = "Buddy";
        var pet = Pet.Create(petId, ownerId, petName);
        
        var expectedDto = new PetDto(
            petId,
            ownerId,
            petName,
            0,
            50,
            50,
            50,
            100
        );

        _petRepository.GetByOwnerIdAsync(ownerId, CancellationToken.None)
            .Returns(pet);

        var query = new GetPetQuery(ownerId);

        // Act
        var result = await _handler.HandleAsync(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedDto);
    }

    [Fact]
    public async Task ShouldFailWhenOwnersPetIsNotFound()
    {
        // Arrange
        var ownerId = Guid.NewGuid();

        _petRepository.GetByOwnerIdAsync(ownerId, CancellationToken.None)
            .Returns((Pet?)null);

        var query = new GetPetQuery(ownerId);

        // Act
        var result = await _handler.HandleAsync(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}