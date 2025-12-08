namespace VirtualPet.Infrastructure.Pet;

using VirtualPet.Domain.Pet;

public class GuidGenerator : IGuidGenerator
{
    public Guid NewGuid()
    {
        return Guid.NewGuid();
    }
}