using Retro.Configuration;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.AddConfig("orders-api");


app.MapGet("/", () => "Hello World!");

app.Run();
