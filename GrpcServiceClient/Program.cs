using GrpcServiceApi;
using GrpcServiceClient.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddGrpcClient<UserService.UserServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcServer:Address"]!);
});

builder.Services.AddScoped<IUserService, ClientUserService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    // Map the OpenAPI document endpoint (e.g., /openapi/v1.json)
    app.MapOpenApi();

    // Enable the Swagger UI middleware, only in development for security best practices
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API V1");
        options.RoutePrefix = "swagger"; // Access UI at /swagger
        options.EnableTryItOutByDefault();
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();