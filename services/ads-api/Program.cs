using Retro.Configuration;
using Retro.Consul.HealthCheck;


var builder = WebApplication.CreateBuilder(args);

builder.AddConfig("ads-api");
builder.AddHeathCheckFor(["ads-api"]);


var app = builder.Build();

app.MapHealthChecks("/health");

app.MapGet("/", () => "Hello World!");

app.Run();
