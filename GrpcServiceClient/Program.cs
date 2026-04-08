using GrpcServiceApi;
using GrpcServiceClient.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddGrpcClient<UserService.UserServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Address"]!);
});

builder.Services.AddScoped<IUserService, ClientUserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();