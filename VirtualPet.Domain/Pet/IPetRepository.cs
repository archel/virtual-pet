namespace VirtualPet.Domain.Pet;

public interface IPetRepository
{
    Task<Pet?> GetByOwnerIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Pet pet, CancellationToken cancellationToken);
}