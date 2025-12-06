
using VirtualPet.Domain.Pet;

namespace VirtualPet.Infrastructure.Pet;

public class InMemoryPetRepository : IPetRepository
{
    private readonly Dictionary<Guid, Domain.Pet.Pet> _pets = [];

    public Task AddAsync(Domain.Pet.Pet pet, CancellationToken cancellationToken)
    {
        _pets.Add(pet.ToDto().OwnerId, pet);
        return Task.CompletedTask;
    }

    Task<Domain.Pet.Pet?> IPetRepository.GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken)
    => _pets.TryGetValue(ownerId, out var pet) ?
            Task.FromResult<Domain.Pet.Pet?>(pet) :
            Task.FromResult<Domain.Pet.Pet?>(null);
}