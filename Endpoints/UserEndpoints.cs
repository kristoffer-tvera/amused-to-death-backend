using System.Security.Claims;
using AmusedToDeath.Backend.Models;
using AmusedToDeath.Backend.Services;

namespace AmusedToDeath.Backend.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/login", async (DbService dbService, BattleNetService battleNetService, TokenService tokenService, LoginRequest loginRequest) =>
        {
            var accessToken = await battleNetService.GetAccessToken(loginRequest.Code);
            if (string.IsNullOrEmpty(accessToken))
            {
                return Results.BadRequest();
            }

            var userInfo = await battleNetService.GetUserInfo(accessToken);
            if (userInfo == null)
            {
                return Results.BadRequest();
            }

            var user = await dbService.GetByQuerySingle<User>("SELECT * FROM Users WHERE BattleTag = @BattleTag", new { userInfo.BattleTag });
            if (user == null)
            {
                user = new User { BattleTag = userInfo.BattleTag };
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

        app.MapGet("/bnet-profile", async (BattleNetService battleNetService, ClaimsPrincipal userClaim) =>
        {
            var accessToken = userClaim.GetAccessToken();
            if (accessToken == null)
            {
                return Results.BadRequest();
            }

            var profileData = await battleNetService.GetProfileAsync(accessToken);
            return Results.Ok(profileData);
        })
        .WithName("Get BattleNet profile data")
        .Produces<WowProfileResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi()
        .RequireAuthorization();
    }
}