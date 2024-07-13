using AmusedToDeath.Backend.Endpoints;
using AmusedToDeath.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddLogging(opt =>
    opt.AddConfiguration(builder.Configuration.GetSection("Logging"))
        .AddConsole()
        .AddSentry()
);

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

app.MapRaidEndpoints();
app.MapUserEndpoints();
app.MapApplicationEndpoints();

using (var scope = app.Services.CreateScope())
{
    var sampleService = scope.ServiceProvider.GetRequiredService<DbService>();
    sampleService.Initialize().Wait();
}

app.Run();
