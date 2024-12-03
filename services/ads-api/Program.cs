using Retro.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.AddConfig("ads-api");



var app = builder.Build();

app.MapHealthChecks("/health");

app.MapGet("/", () => "Hello World!");

app.Run();
