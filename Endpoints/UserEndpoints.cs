using AmusedToDeath.Backend.Models;
using AmusedToDeath.Backend.Services;

namespace AmusedToDeath.Backend.Endpoints;

public static class UserEndpoints
{

    /// <summary>
    /// Maps the following endpoints: "Add character", "Update character", "Delete character", "Get characters", "Get character by id"
    /// </summary>
    /// <param name="app"></param>
    public static void MapUserEndpoints(this WebApplication app)
    {

        app.MapGet("/characters", async (DbService dbService) =>
        {
            return await dbService.GetAll<Character>();
        })
        .WithName("Get all characters")
        .Produces<List<Character>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Characters")
        .RequireAuthorization();

        app.MapGet("/characters/{id}", async (DbService dbService, int id) =>
        {
            return await dbService.Get<Character>(id);
        })
        .WithName("Get user by id")
        .Produces<Character>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Characters")
        .RequireAuthorization();

        app.MapPost("/characters", async (DbService dbService, Character character) =>
        {
            return await dbService.Insert(character);
        })
        .WithName("Create user")
        .Produces<Character>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Characters")
        .AllowAnonymous();

        app.MapPut("/characters/{id}", async (DbService dbService, int id, Character character) =>
        {
            return await dbService.Update(character);
        })
        .WithName("Update user")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Characters")
        .RequireAuthorization();

        app.MapDelete("/characters/{id}", async (DbService dbService, int id) =>
        {
            return await dbService.Delete(new Character { Id = id });
        })
        .WithName("Delete user")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Characters")
        .RequireAuthorization();

    }
}