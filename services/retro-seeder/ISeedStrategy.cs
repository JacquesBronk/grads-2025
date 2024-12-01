namespace Retro.Seeder;

public interface ISeedStrategy
{
    Task<Job> SeedAsync(CancellationToken cancellationToken = default);
}