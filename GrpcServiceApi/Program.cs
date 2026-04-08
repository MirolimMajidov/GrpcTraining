using GrpcServiceApi.Entities;
using GrpcServiceApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.AddServiceDefaults();

builder.Services.AddSingleton<IUserManagerService, UserManagerService>();

var app = builder.Build();

// Seed initial users
var userManager = app.Services.GetRequiredService<IUserManagerService>();
userManager.Create(new User { FirstName = "Alice", LastName = "Smith", Age = 30 });
userManager.Create(new User { FirstName = "Bob", LastName = "Johnson", Age = 25 });

// Configure the HTTP request pipeline.
app.MapGrpcService<ServerUserService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();