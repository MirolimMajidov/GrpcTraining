var builder = DistributedApplication.CreateBuilder(args);
var server = builder.AddProject("service-api", "../GrpcServiceApi/GrpcServiceApi.csproj");
builder.AddProject("service-client", "../GrpcServiceClient/GrpcServiceClient.csproj")
    .WithReference(server)
    .WaitFor(server);

builder.Build().Run();