using System.Security.Claims;
using AmusedToDeath.Backend.Models;
using AmusedToDeath.Backend.Services;

namespace AmusedToDeath.Backend.Endpoints;

public static class CharacterEndPoints
{
    public static void MapCharacterEndPoints(this WebApplication app)
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
        .WithName("Get character by id")
        .Produces<Character>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Characters")
        .RequireAuthorization();

        app.MapGet("/characters/my", async (DbService dbService, ClaimsPrincipal userClaim) =>
        {
            var userId = userClaim.GetUserId();
            if (userId == null)
            {
                return Results.BadRequest();
            }

            var character = await dbService.GetByQuery<Character>($"SELECT * FROM Characters WHERE OwnerId = {userId.Value}");
            return Results.Ok(character);
        })
        .WithName("Get MY characterS")
        .Produces<List<Character>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Characters")
        .RequireAuthorization();

        app.MapPost("/characters", async (DbService dbService, Character character, ClaimsPrincipal userClaim) =>
        {
            var userId = userClaim.GetUserId();
            if (userId == null)
            {
                return Results.BadRequest();
            }

            character.OwnerId = userId.Value;
            character.AddedDate = DateTime.UtcNow;
            character.ChangedDate = DateTime.UtcNow;

            character.Id = await dbService.Insert(character);
            return Results.Created($"/characters/{character.Id}", character);
        })
        .WithName("Create character")
        .Produces<Character>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Characters")
        .RequireAuthorization();

        app.MapPut("/characters/{id}", async (DbService dbService, int id, Character character) =>
        {
            return await dbService.Update(character);
        })
        .WithName("Update character")
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
        .WithName("Delete character")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Characters")
        .RequireAuthorization();



    }
}