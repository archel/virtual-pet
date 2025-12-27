using VirtualPet.Domain.Pet;

namespace VirtualPet.Application.Commands;

public record CreatePetCommand(
    Guid OwnerId,
    string Name
) : ICommand;


public class CreatePetCommandHandler(IPetRepository petRepository, IGuidGenerator guidGenerator) : ICommandHandler<CreatePetCommand>
{
    public async Task<Result> HandleAsync(CreatePetCommand command, CancellationToken cancellationToken)
    {

        if (await petRepository.GetByOwnerIdAsync(command.OwnerId, cancellationToken) is null)
        {
            var pet = Pet.Create(guidGenerator.NewGuid(), command.OwnerId, command.Name);
            await petRepository.AddAsync(pet, cancellationToken);
            return Result.Success();
        }

        return Result.Failure(OwnerAlreadyHasPetException.Create(command.OwnerId.ToString()));
    }
}