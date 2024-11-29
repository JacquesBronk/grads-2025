using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Retro.Auth;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<GamersDbContext>(options => options.UseInMemoryDatabase("LOYAL_GAMERS"));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Loyal Gamers API", Version = "v1" });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIdentity<LoyalGamers, IdentityRole<Guid>>(options =>
    {
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(45);
        options.Lockout.AllowedForNewUsers = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 3;
        options.Password.RequiredUniqueChars = 0;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.Tokens.AuthenticatorIssuer = "com.retro-auth-api";
        options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
        options.User.RequireUniqueEmail = false;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<GamersDbContext>()
    .AddTokenProvider("com.retro.issuer", typeof(DataProtectorTokenProvider<LoyalGamers>));

builder.Services.AddSingleton<IEmailSender<LoyalGamers>, NullEmailSender<LoyalGamers>>();


builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapIdentityApi<LoyalGamers>();


app.MapGet("/whoami", (ClaimsPrincipal user) => user.Identity?.Name)
    .RequireAuthorization()
    .WithDescription("Returns the name of the authenticated user.");

app.Run();