using System.Text;

namespace Retro.Seeder.ConsulSeeder;

public class ConsulStrategy(IWebHostEnvironment environment, ILogger logger) : ISeedStrategy
{
    private readonly string _baseUrl = environment.IsDevelopment() ? "http://localhost:8500" : "http://consul:8500";

    public async Task<Job> SeedAsync(CancellationToken cancellationToken = default)
    {
        Guid jobId = Guid.NewGuid();
        logger.LogInformation("Seeding Consul");

        string absolutePath = Path.Combine("seed", "consul");
        if (environment.IsDevelopment())
        {
            absolutePath = Path.GetFullPath(Path.Combine(environment.ContentRootPath, "..", "init"));
        }

        if (!Directory.Exists(absolutePath))
        {
            logger.LogWarning($"No seed data found at {absolutePath}");
            return new Job
            {
                JobName = "Consul",
                IsCompleted = false,
                Exception = new DirectoryNotFoundException("seeding data not found"),
                JobId = jobId,
                StartTime = DateTime.UnixEpoch
            };
        }

        FileCache fileCache = new FileCache();
        var files = fileCache.GetFiles(absolutePath);

        if (files == null)
        {
            throw new FileNotFoundException("config not found");
        }

        foreach (var file in files)
        {
            var key = $"app-settings/{Path.GetFileNameWithoutExtension(file)}";
            var value = await File.ReadAllTextAsync(file, cancellationToken);

            // Create the content without additional serialization
            var content = new StringContent(value, Encoding.UTF8, "text/plain");

            // Send the request using HttpClient directly
            using var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.PutAsync($"/v1/kv/{key}", content, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                logger.LogError($"Failed to seed {key}: {response.StatusCode} - {errorContent}");
                return new Job
                {
                    JobName = "Consul",
                    IsCompleted = false,
                    Exception = new Exception($"Failed to seed {key}: {response.StatusCode} - {errorContent}"),
                    JobId = jobId,
                    StartTime = DateTime.UnixEpoch
                };
            }
        }

        return new Job
        {
            JobName = "Consul",
            IsCompleted = true,
            JobId = jobId,
            StartTime = DateTime.UnixEpoch
        };
    }
}
