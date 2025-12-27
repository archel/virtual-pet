using VirtualPet.Application.Commands;
using VirtualPet.Application.Queries;
using VirtualPet.Domain.Pet;
using VirtualPet.Presentation.Requests;

namespace VirtualPet.Presentation.Endpoints;

public static class VirtualPetEndpoints
{
    public static void MapVirtualPetEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/pets")
            .WithName("Virtual Pets");

        group.MapGet("/", GetPets)
            .WithName("Get all pets");

        group.MapGet("/{id:guid}", GetPetByOwnerId)
            .WithName("Get pet by owner id");

        group.MapPost("/", CreatePet)
            .WithName("Create pet");

        group.MapPut("/{id}", UpdatePet)
            .WithName("Update pet by owner id");

        group.MapDelete("/{id}", DeletePet)
            .WithName("Delete pet by owner id");
    }

    private static IResult GetPets() => Results.Ok();

    private static IResult GetPetByOwnerId(Guid id, IQueryHandler<GetPetQuery, PetDto> getPetQueryHandler) {
        var query = new GetPetQuery(id);
        var result = getPetQueryHandler.HandleAsync(query, CancellationToken.None).GetAwaiter().GetResult();

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.NotFound();
    }

    private static IResult CreatePet(ICommandHandler<CreatePetCommand> createPetCommandHandler, CreatePetRequest request) {
        var command = request.ToCommand();
        var result = createPetCommandHandler.HandleAsync(command, CancellationToken.None).GetAwaiter().GetResult();
        
        return result.IsSuccess
            ? Results.Created($"/api/pets/{command.OwnerId}", null)
            : Results.Problem(detail: result.Error?.Message, statusCode: StatusCodes.Status400BadRequest);
    }

    private static IResult UpdatePet(Guid id) => Results.NoContent();

    private static IResult DeletePet(Guid id) => Results.NoContent();
}