using VirtualPet.Domain.Pet;

namespace VirtualPet.Application.Queries;

public record GetPetQuery(Guid OwnerId) : IQuery<PetDto>;

public class GetPetQueryHandler(IPetRepository petRepository) : IQueryHandler<GetPetQuery, PetDto>
{
    public async Task<Result<PetDto>> HandleAsync(GetPetQuery query, CancellationToken cancellationToken)
    {
        var pet = await petRepository.GetByOwnerIdAsync(query.OwnerId, cancellationToken);

        return pet is null
            ? Result<PetDto>.Failure(PetNotFoundException.Create(query.OwnerId.ToString()))
            : Result<PetDto>.Success(pet.ToDto());
    }
}