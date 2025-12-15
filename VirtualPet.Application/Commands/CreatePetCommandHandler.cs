using VirtualPet.Domain.Pet;

namespace VirtualPet.Application.Commands;

public record CreatePetCommand(
    Guid OwnerId,
    string Name
) : ICommand;


public class CreatePetCommandHandler(IPetRepository petRepository, IGuidGenerator guidGenerator) : ICommandHandler<CreatePetCommand>
{
    public async Task HandleAsync(CreatePetCommand command, CancellationToken cancellationToken)
    {

        if (await petRepository.GetByOwnerIdAsync(command.OwnerId, cancellationToken) is null)
        {
            var pet = Pet.Create(guidGenerator.NewGuid(), command.OwnerId, command.Name);
            await petRepository.AddAsync(pet, cancellationToken);
            return;
        }

        throw new InvalidOperationException("Owner already has a pet.");
    }
}