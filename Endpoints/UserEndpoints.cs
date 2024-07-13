using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using AmusedToDeath.Backend.Models;
using AmusedToDeath.Backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AmusedToDeath.Backend.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/battle-net-redirect", async (DbService dbService, BattleNetService battleNetService, TokenService tokenService, string code, string state) =>
        {
            var accessToken = await battleNetService.GetAccessToken(code);
            if (string.IsNullOrEmpty(accessToken))
            {
                return Results.BadRequest();
            }

            var userInfo = await battleNetService.GetUserInfo(accessToken);
            if (userInfo == null)
            {
                return Results.BadRequest();
            }

            var user = await dbService.Get<User>(userInfo.Id);
            if (user == null)
            {
                user = new User { Id = userInfo.Id, BattleTag = userInfo.BattleTag };
                user.Id = await dbService.Insert(user);
            }

            var jwt = tokenService.GenerateToken(user, accessToken);
            return Results.Ok(jwt);
        })
        .WithName("BattleNet redirect")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .AllowAnonymous();


        app.MapGet("/characters", async (DbService dbService, ClaimsPrincipal userClaim) =>
        {
            var user = userClaim.Identity.Name;
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

        app.MapPost("/characters", async (DbService dbService, Character character, ClaimsPrincipal userClaim) =>
        {
            var user = dbService.Get<User>(int.Parse(userClaim.Identity.Name));
            return await dbService.Insert(character);
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