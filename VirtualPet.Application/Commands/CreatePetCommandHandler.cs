using VirtualPet.Domain.Pet;

namespace VirtualPet.Application.Commands;

public record CreatePetCommand(
    Guid OwnerId,
    string Name
) : ICommand;


public class CreatePetCommandHandler(IPetRepository petRepository) : ICommandHandler<CreatePetCommand>
{
    private readonly IPetRepository _petRepository = petRepository;

    public async Task HandleAsync(CreatePetCommand command, CancellationToken cancellationToken)
    {   

        if (await _petRepository.GetByOwnerIdAsync(command.OwnerId, cancellationToken) is null)
        {
            var pet = Pet.Create(command.OwnerId, command.Name);
            await _petRepository.AddAsync(pet, cancellationToken);
            return;
        }
        
        throw new InvalidOperationException("Owner does not exist.");        
    }
}