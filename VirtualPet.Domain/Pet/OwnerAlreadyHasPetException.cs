namespace VirtualPet.Domain.Pet;

public class OwnerAlreadyHasPetException(string ownerId) : Exception($"Owner with id {ownerId} already has a pet.")
{
    public static OwnerAlreadyHasPetException Create(string ownerId) => new(ownerId);
}