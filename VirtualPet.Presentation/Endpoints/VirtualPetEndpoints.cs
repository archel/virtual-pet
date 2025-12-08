using VirtualPet.Application.Commands;
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

        group.MapGet("/{id}", GetPetById)
            .WithName("Get pet by ID");

        group.MapPost("/", CreatePet)
            .WithName("Create pet");

        group.MapPut("/{id}", UpdatePet)
            .WithName("Update pet");

        group.MapDelete("/{id}", DeletePet)
            .WithName("Delete pet");
    }

    private static IResult GetPets() => Results.Ok();

    private static IResult GetPetById(Guid id) => Results.Ok();

    private static IResult CreatePet(ICommandHandler<CreatePetCommand> createPetCommandHandler, CreatePetRequest request) {
        var command = request.ToCommand();
        createPetCommandHandler.HandleAsync(command, CancellationToken.None).GetAwaiter().GetResult();
        return Results.Created($"/api/pets/{command.OwnerId}", null);
    }

    private static IResult UpdatePet(Guid id) => Results.NoContent();

    private static IResult DeletePet(Guid id) => Results.NoContent();
}