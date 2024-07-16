using System.Text;
using AmusedToDeath.Backend.Endpoints;
using AmusedToDeath.Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddHttpClient();
builder.Services.AddLogging(opt =>
    opt.AddConfiguration(builder.Configuration.GetSection("Logging"))
        .AddConsole()
        .AddSentry()
);
builder.Services.AddTransient<DbService>();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddTransient<BattleNetService>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "default_key")),
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapApplicationEndpoints();
app.MapCharacterEndPoints();
app.MapRaidEndpoints();
app.MapUserEndpoints();

// app.MapGet("/bnet", () =>
// {
//     return Results.Redirect("https://oauth.battle.net/authorize?response_type=code&scope=openid&state=69&redirect_uri=http://localhost:5281/battle-net-redirect&client_id=8183bda55fd54566827c595947b189fe");
// });

using (var scope = app.Services.CreateScope())
{
    var sampleService = scope.ServiceProvider.GetRequiredService<DbService>();
    sampleService.Initialize().Wait();
}

app.Run();
