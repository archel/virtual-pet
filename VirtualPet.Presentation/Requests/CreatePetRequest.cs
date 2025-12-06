using VirtualPet.Application.Commands;

namespace VirtualPet.Presentation.Requests;

public record CreatePetRequest(
    Guid OwnerId,
    string Name
)
{
    public CreatePetCommand ToCommand() => new(
        this.OwnerId,
        this.Name
    );
};