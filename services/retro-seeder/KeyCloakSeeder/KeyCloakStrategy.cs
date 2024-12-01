using System.Net;
using System.Text;
using System.Text.Json;
using Retro.Http;

namespace Retro.Seeder.KeyCloakSeeder;

public class KeyCloakStrategy(IWebHostEnvironment environment, ILogger logger) : ISeedStrategy
{
    private readonly string _baseUrl =
        environment.IsDevelopment() ? "http://localhost:8080" : "http://keycloak_web:8080";

    public async Task<Job> SeedAsync(CancellationToken cancellationToken = default)
    {
        Guid jobId = Guid.NewGuid();
        logger.LogInformation("Seeding Keycloak");

        string absolutePath = Path.Combine("seed", "consul");
        if (environment.IsDevelopment())
        {
            absolutePath = Path.GetFullPath(Path.Combine(environment.ContentRootPath, "..", "..", "init"));
        }

        if (!Directory.Exists(absolutePath))
        {
            logger.LogWarning($"No seed data found at {absolutePath}");
            return new Job
            {
                JobName = "Keycloak",
                IsCompleted = false,
                Exception = new DirectoryNotFoundException("Seeding data not found"),
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }

        FileCache fileCache = new FileCache();
        fileCache.GetFiles(absolutePath);

        var realmConfig = fileCache.ReadFileByName("keycloak-realm");
        var clientConfig = fileCache.ReadFileByName("keycloak-realm-client");
        var userConfig = fileCache.ReadFileByName("keycloak-realm-user");

        try
        {
            // Get admin token
            string tokenUrl = $"{_baseUrl}/realms/master/protocol/openid-connect/token";
            var tokenResponse = await new RequestBuilder()
                .For(new Uri(tokenUrl))
                .WithMethod(HttpMethod.Post)
                .WithContent(new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "client_id", "admin-cli" },
                    { "username", "admin" },
                    { "password", "admin" },
                    { "grant_type", "password" }
                }))
                .EnsureStatusCode()
                .Build()
                .GetStringResultAsync(cancellationToken);

            var auth = GetTokenFromAuthResponse(tokenResponse);

            if (string.IsNullOrEmpty(auth.AccessToken))
            {
                logger.LogError("Failed to get access token");
                return new Job
                {
                    JobName = "Keycloak",
                    IsCompleted = false,
                    Exception = new Exception("Failed to get access token"),
                    JobId = jobId,
                    StartTime = DateTime.UtcNow
                };
            }

            string? realmName = GetRealmFromJsonConfig(realmConfig);
            if (string.IsNullOrWhiteSpace(realmName))
            {
                logger.LogError("Failed to get realm from config");
                return new Job
                {
                    JobName = "Keycloak",
                    IsCompleted = false,
                    Exception = new Exception("Failed to get realm from config"),
                    JobId = jobId,
                    StartTime = DateTime.UtcNow
                };
            }

            // Create Realm
            await CreateRealmAsync(realmConfig, auth.AccessToken, cancellationToken);

            // Create Client
            await CreateClientAsync(clientConfig, realmName, auth.AccessToken, cancellationToken);

            // Create Users and Set Passwords
            await CreateUsersAsync(userConfig, realmName, auth.AccessToken, cancellationToken);

            logger.LogInformation("Keycloak seeding completed successfully.");
            return new Job
            {
                JobName = "Keycloak",
                IsCompleted = true,
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while seeding Keycloak.");
            return new Job
            {
                JobName = "Keycloak",
                IsCompleted = false,
                Exception = ex,
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }
    }

    private async Task CreateRealmAsync(string realmConfig, string accessToken, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating realm...");

        var realmRequest = new RequestBuilder()
            .For(new Uri($"{_baseUrl}/admin/realms"))
            .WithMethod(HttpMethod.Post)
            .WithContent(new StringContent(realmConfig, Encoding.UTF8, "application/json"))
            .EnsureStatusCode([HttpStatusCode.Created, HttpStatusCode.Conflict, HttpStatusCode.Accepted])
            .WithAuthentication(new OAuth2Authentication(accessToken))
            .WhenFailed(response =>
            {
                logger.LogError("Failed to create realm: {Response}", response);
                return Task.CompletedTask;
            })
            .Build();

        await realmRequest.GetStatusCodeAsync(cancellationToken);
        logger.LogInformation("Realm created successfully.");
    }

    private async Task CreateClientAsync(string clientConfig, string realmName, string accessToken,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating client...");
        var clientRequest = new RequestBuilder()
            .For(new Uri($"{_baseUrl}/admin/realms/{realmName}/clients"))
            .WithMethod(HttpMethod.Post)
            .WithContent(new StringContent(clientConfig, Encoding.UTF8, "application/json"))
            .EnsureStatusCode([HttpStatusCode.Created, HttpStatusCode.Conflict, HttpStatusCode.Accepted])
            .WithAuthentication(new OAuth2Authentication(accessToken))
            .Build();

        await clientRequest.GetStatusCodeAsync(cancellationToken);
        logger.LogInformation("Client created successfully.");
    }

    private async Task CreateUsersAsync(string userConfig, string realmName, string accessToken,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating users...");
        JsonDocument doc = JsonDocument.Parse(userConfig);
        foreach (var userElement in doc.RootElement.EnumerateArray())
        {
            var userContent = new StringContent(userElement.GetRawText(), Encoding.UTF8, "application/json");
            var userRequest = new RequestBuilder()
                .For(new Uri($"{_baseUrl}/admin/realms/{realmName}/users"))
                .WithMethod(HttpMethod.Post)
                .WithContent(userContent)
                .EnsureStatusCode([HttpStatusCode.Created, HttpStatusCode.Conflict, HttpStatusCode.Accepted, HttpStatusCode.NoContent])
                .WithAuthentication(new OAuth2Authentication(accessToken))
                .Build();

            await userRequest.GetStatusCodeAsync(cancellationToken);
            logger.LogInformation($"User '{userElement.GetProperty("username").GetString()}' created successfully.");

            // Set user password
            await SetUserPasswordAsync(userElement, realmName, accessToken, cancellationToken);
        }

        logger.LogInformation("Users created successfully.");
    }

    private async Task SetUserPasswordAsync(JsonElement userElement, string realmName, string accessToken,
        CancellationToken cancellationToken)
    {
        string username = userElement.GetProperty("username").GetString();
        string password = userElement.GetProperty("credentials")[0].GetProperty("value").GetString();

        // Get the user ID
        var getUsersRequest = new RequestBuilder()
            .For(new Uri($"{_baseUrl}/admin/realms/{realmName}/users?username={username}"))
            .WithMethod(HttpMethod.Get)
            .EnsureStatusCode([HttpStatusCode.Created, HttpStatusCode.Conflict, HttpStatusCode.Accepted, HttpStatusCode.NoContent])
            .WithAuthentication(new OAuth2Authentication(accessToken))
            .Build();

        string usersResponse = await getUsersRequest.GetStringResultAsync(cancellationToken);
        var users = JsonSerializer.Deserialize<List<JsonElement>>(usersResponse);
        string userId = users[0].GetProperty("id").GetString();

        // Set the password
        var passwordPayload = new
        {
            type = "password",
            temporary = false,
            value = password
        };

        var setPasswordRequest = new RequestBuilder()
            .For(new Uri($"{_baseUrl}/admin/realms/{realmName}/users/{userId}/reset-password"))
            .WithMethod(HttpMethod.Put)
            .WithContent(
                new StringContent(JsonSerializer.Serialize(passwordPayload), Encoding.UTF8, "application/json"))
            .EnsureStatusCode([HttpStatusCode.Created, HttpStatusCode.Conflict, HttpStatusCode.Accepted, HttpStatusCode.NoContent])
            .WithAuthentication(new OAuth2Authentication(accessToken))
            .Build();

        await setPasswordRequest.GetStringResultAsync(cancellationToken);
        logger.LogInformation($"Password set for user '{username}'.");
    }

    private string? GetRealmFromJsonConfig(string config)
    {
        JsonDocument doc = JsonDocument.Parse(config);
        var root = doc.RootElement;
        return root.GetProperty("realm").GetString();
    }

    private (string? AccessToken, string? RefreshToken) GetTokenFromAuthResponse(string bearerTokenResponse)
    {
        JsonDocument doc = JsonDocument.Parse(bearerTokenResponse);
        var root = doc.RootElement;
        return (
            root.GetProperty("access_token").GetString(),
            root.GetProperty("refresh_token").GetString()
        );
    }
}