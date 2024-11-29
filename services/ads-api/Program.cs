using Retro.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfig("ads-api");

builder.Environment.EnvironmentName = builder.Configuration["Environment"];

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
