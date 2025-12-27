namespace VirtualPet.Domain.Pet;

public class PetNotFoundException(string ownerId) : Exception($"Pet for owner with id {ownerId} was not found.")
{
    public static PetNotFoundException Create(string ownerId) => new(ownerId);
}