using Microsoft.AspNetCore.Builder;

namespace VirtualPet.Presentation.Endpoints;

public static class VirtualPetEndpoints
{
    public static void MapVirtualPetEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/pets")
            .WithName("Virtual Pets")
            .WithOpenApi();

        group.MapGet("/", GetPets)
            .WithName("Get all pets")
            .WithOpenApi();

        group.MapGet("/{id}", GetPetById)
            .WithName("Get pet by ID")
            .WithOpenApi();

        group.MapPost("/", CreatePet)
            .WithName("Create pet")
            .WithOpenApi();

        group.MapPut("/{id}", UpdatePet)
            .WithName("Update pet")
            .WithOpenApi();

        group.MapDelete("/{id}", DeletePet)
            .WithName("Delete pet")
            .WithOpenApi();
    }

    private static IResult GetPets() => Results.Ok();

    private static IResult GetPetById(int id) => Results.Ok();

    private static IResult CreatePet() => Results.Created();

    private static IResult UpdatePet(int id) => Results.NoContent();

    private static IResult DeletePet(int id) => Results.NoContent();
}