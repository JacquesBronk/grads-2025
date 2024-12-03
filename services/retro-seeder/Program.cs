using Retro.Seeder;
using Retro.Seeder.ConsulSeeder;
using Retro.Seeder.KeyCloakSeeder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var app = builder.Build();

//seed the consul
ISeedStrategy consulStrategy = new ConsulStrategy(app.Environment, app.Logger);
var consulJob = await consulStrategy.SeedAsync();

Console.WriteLine($"Consul seeding completed: {consulJob.IsCompleted}");

// seed the keycloak
ISeedStrategy keyCloakStrategy = new KeyCloakStrategy(app.Environment, app.Logger);
var keyCloakJob = await keyCloakStrategy.SeedAsync();

Console.WriteLine($"KeyCloak seeding completed: {keyCloakJob.IsCompleted}");

app.MapHealthChecks("health");

app.Run();


