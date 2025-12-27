using FluentAssertions;

using NSubstitute;

using VirtualPet.Application.Commands;
using VirtualPet.Domain.Pet;

namespace VirtualPet.Tests.Unit.Application.Commands;

public class CreatePetTest
{
    private readonly CreatePetCommandHandler _handler;
    private readonly IPetRepository _petRepository;
    private const string A_PET_GUID = "11111111-1111-1111-1111-111111111111";
    public CreatePetTest()
    {
        var mockGuidGenerator = Substitute.For<IGuidGenerator>();
        _petRepository = Substitute.For<IPetRepository>();
        _handler = new CreatePetCommandHandler(_petRepository, mockGuidGenerator);
        mockGuidGenerator.NewGuid().Returns(Guid.Parse(A_PET_GUID));
    }
    [Fact]
    public async Task Should_Create_Pet()
    {
        // Arrange
        var petGuid = Guid.Parse(A_PET_GUID);
        var ownerId = Guid.NewGuid();
        var command = new CreatePetCommand(ownerId, "Buddy");
        var cancellationToken = CancellationToken.None;
        _petRepository.GetByOwnerIdAsync(ownerId, cancellationToken).Returns((Pet?)null);
        Pet? capturedPet = null;
        await _petRepository
            .AddAsync(
                Arg.Do<Pet>(pet => capturedPet = pet),
                Arg.Is<CancellationToken>(ct => ct == cancellationToken)
            );

        // Act
        var result = await _handler.HandleAsync(command, cancellationToken);
        
        // Assert
        var expectedPet = Pet.Create(petGuid, ownerId, "Buddy");
        capturedPet.Should().BeEquivalentTo(expectedPet);
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Should_Not_Create_Pet_When_Owner_Has_A_Pet()
    {
        // Arrange
        var petGuid = Guid.Parse(A_PET_GUID);
        var ownerId = Guid.NewGuid();
        var command = new CreatePetCommand(ownerId, "Buddy");
        var existingPet = Pet.Create(petGuid, ownerId, "Buddy");
        var cancellationToken = CancellationToken.None;
        _petRepository.GetByOwnerIdAsync(ownerId, cancellationToken).Returns(existingPet);

        var result = await _handler.HandleAsync(command, cancellationToken);
        
        // Act & Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().BeEquivalentTo(OwnerAlreadyHasPetException.Create(ownerId.ToString()));
    }
}