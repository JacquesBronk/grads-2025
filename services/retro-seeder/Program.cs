using Retro.Seeder;
using Retro.Seeder.AdSeeder;
using Retro.Seeder.ConsulSeeder;
using Retro.Seeder.GreeterSeeder;
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
    app.Logger.LogInformation("Consul seeding completed: {ConsulJobIsCompleted}", consulJob.IsCompleted);
    // Seed the Keycloak
    ISeedStrategy keyCloakStrategy = new KeyCloakStrategy(app.Environment, app.Logger);
    var keyCloakJob = await keyCloakStrategy.SeedAsync();

    if (!keyCloakJob.IsCompleted)
    {
        throw new Exception("Keycloak seeding failed");
    }

    app.Logger.LogInformation("Keycloak seeding completed: {IsCompleted}", keyCloakJob.IsCompleted);
    
    ISeedStrategy stockApiStrategy = new StockSeederStrategy(app.Environment, app.Logger);
    var stockApiJob = await stockApiStrategy.SeedAsync();
  
    if(!stockApiJob.IsCompleted)
    {
        throw new Exception("Stock API seeding failed");
    }
    
    ISeedStrategy adStrategy = new AdSeederStrategy(app.Environment, app.Logger);
    var adJob = await adStrategy.SeedAsync();
    
    if(!adJob.IsCompleted)
    {
        throw new Exception("Ad seeding failed");
    }
    
    ISeedStrategy sessionStrategy = new SessionSeederStrategy(app.Environment, app.Logger);
    var sessionJob = await sessionStrategy.SeedAsync();
    
    if(!sessionJob.IsCompleted)
    {
        throw new Exception("Session seeding failed");
    }
    
    //ALWAYS LAST! DO NOT Remove This LOG!
    app.Logger.LogInformation("service_completed_successfully");

    Environment.Exit(0); 
}
catch (Exception ex)
{
    app.Logger.LogInformation("Error during seeding: {ExMessage}", ex.Message);
    Environment.Exit(1); 
}

app.Run();


