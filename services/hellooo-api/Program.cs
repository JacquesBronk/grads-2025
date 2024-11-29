using Retro.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfig("greeter-api");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
