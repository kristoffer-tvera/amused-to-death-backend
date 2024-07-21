using AmusedToDeath.Backend.Models;
using AmusedToDeath.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmusedToDeath.Backend.Endpoints;



public static class ApplicationEndpoints
{

    public static void MapApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/applications", async (DbService dbService) =>
        {
            var applications = await dbService.GetAll<Application>();

            foreach (var application in applications)
            {
                application.ChangeKey = "";
            }

            return await dbService.GetAll<Application>();
        })
        .WithName("Get all applications")
        .Produces<List<Raid>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Applications")
        .RequireAuthorization();

        app.MapGet("/applications/{id}", async (DbService dbService, int id) =>
        {
            var application = await dbService.Get<Application>(id);
            application.ChangeKey = "";
            return application;
        })
        .WithName("Get application by id")
        .Produces<Application>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Applications");
        // .RequireAuthorization();

        app.MapPost("/applications", async (DbService dbService, Application application) =>
        {
            application.ChangeKey = Guid.NewGuid().ToString();
            application.AddedDate = DateTime.UtcNow;
            application.ChangedDate = DateTime.UtcNow;
            application.Id = await dbService.Insert(application);
            return Results.Created($"/applications/{application.Id}", application);
        })
        .WithName("Create application")
        .Produces<Application>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Applications")
        .AllowAnonymous();

        app.MapPut("/applications/{id}", async ([FromBody] Application application, int id, DbService dbService) =>
        {
            var app = await dbService.Get<Application>(id);
            if (app == null)
            {
                return Results.NotFound();
            }

            if (app.ChangeKey != application.ChangeKey)
            {
                return Results.BadRequest("Change key does not match");
            }

            await dbService.Update(application);
            return Results.Ok();
        })
        .WithName("Update application")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .WithTags("Applications")
        .AllowAnonymous();


    }
}