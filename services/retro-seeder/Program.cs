using Retro.Seeder;
using Retro.Seeder.ConsulSeeder;
using Retro.Seeder.KeyCloakSeeder;
using Retro.Seeder.StockSeeder;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

string logFilePath = "/tmp/seeder.log";
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(logFilePath)
    .CreateLogger();

builder.Logging.AddSerilog(Log.Logger);

var app = builder.Build();

try
{

    ISeedStrategy consulStrategy = new ConsulStrategy(app.Environment, app.Logger);
    var consulJob = await consulStrategy.SeedAsync();

    if (!consulJob.IsCompleted)
    {
        throw new Exception("Consul seeding failed");
    }
    app.Logger.LogInformation($"Consul seeding completed: {consulJob.IsCompleted}");
    // Seed the Keycloak
    ISeedStrategy keyCloakStrategy = new KeyCloakStrategy(app.Environment, app.Logger);
    var keyCloakJob = await keyCloakStrategy.SeedAsync();

    if (!keyCloakJob.IsCompleted)
    {
        throw new Exception("Keycloak seeding failed");
    }

    app.Logger.LogInformation($"Keycloak seeding completed: {keyCloakJob.IsCompleted}");
    
    ISeedStrategy stockApiStrategy = new StockSeederStrategy(app.Environment, app.Logger);
    var stockApiJob = await stockApiStrategy.SeedAsync();
    
    if(!stockApiJob.IsCompleted)
    {
        throw new Exception("Stock API seeding failed");
    }
    
    //ALWAYS LAST! DO NOT Remove This LOG!
    app.Logger.LogInformation("service_completed_successfully");

    Environment.Exit(0); 
}
catch (Exception ex)
{
    app.Logger.LogInformation($"Error during seeding: {ex.Message}");
    Environment.Exit(1); 
}

app.Run();


