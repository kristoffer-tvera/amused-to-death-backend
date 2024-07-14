using AmusedToDeath.Backend.Models;
using AmusedToDeath.Backend.Services;

namespace AmusedToDeath.Backend.Endpoints;

public static class RaidEndpoints
{

    public static void MapRaidEndpoints(this WebApplication app)
    {
        app.MapGet("/raids", async (DbService dbService) =>
        {
            return await dbService.GetAll<Raid>();
        })
        .WithName("GetAllRaids")
        .Produces<List<Raid>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Raids")
        .RequireAuthorization();

        app.MapGet("/raids/{id}", async (DbService dbService, int id) =>
        {
            return await dbService.Get<Raid>(id);
        })
        .WithName("GetRaidById")
        .Produces<Raid>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Raids")
        .RequireAuthorization();


        app.MapPost("/raids", async (DbService dbService, Raid raid) =>
        {

            raid.AddedDate = DateTime.UtcNow;
            raid.ChangedDate = DateTime.UtcNow;
            return await dbService.Insert(raid);
        })
        .WithName("CreateRaid")
        .Produces<Raid>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Raids")
        .RequireAuthorization("officer");

        app.MapPut("/raids/{id}", async (DbService dbService, int id, Raid raid) =>
        {
            return await dbService.Update(raid);
        })
        .WithName("UpdateRaid")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Raids")
        .RequireAuthorization("officer");

        app.MapDelete("/raids/{id}", async (DbService dbService, int id) =>
        {
            return await dbService.Delete(new Raid { Id = id });
        })
        .WithName("DeleteRaid")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Raids")
        .RequireAuthorization("officer");

    }

}