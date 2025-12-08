using NSubstitute;

using Shouldly;

using VirtualPet.Application.Commands;
using VirtualPet.Domain.Pet;

namespace VirtualPet.Tests.Unit.Application.Command;

public class CreatePetCommandTest
{
    private readonly CreatePetCommandHandler _handler;
    private readonly IPetRepository _petRepository;
    private const string A_PET_GUID = "11111111-1111-1111-1111-111111111111";
    public CreatePetCommandTest()
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

        // Act
        await _handler.HandleAsync(command, CancellationToken.None);
        _petRepository.GetByOwnerIdAsync(ownerId, CancellationToken.None).Returns((Pet?)null);

        // Assert
        var expectedPet = Pet.Create(petGuid, ownerId, "Buddy");
        await _petRepository.Received(1).AddAsync(
            Arg.Is<Pet>(p => p.Equals(expectedPet)),
            Arg.Any<CancellationToken>()
        );
    }
}