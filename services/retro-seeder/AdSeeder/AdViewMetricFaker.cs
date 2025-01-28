

using Bogus;
using Retro.Ad.Domain;

namespace Retro.Seeder.AdSeeder;

public class AdViewMetricFaker
{
    public static List<AdViewMetric> GenerateAdMetrics(int count)
    {
        var faker = new Faker<AdViewMetric>()
            .RuleFor(avm => avm.AdId, _ => Guid.NewGuid())
            .RuleFor(avm => avm.UserId, f => f.Random.Guid().ToString())
            .RuleFor(avm => avm.IpAddress, f => f.Internet.Ip())
            .RuleFor(avm => avm.UserAgent, f => f.Internet.UserAgent())
            .RuleFor(avm => avm.ViewedAt, f => f.Date.Past());
        
        return faker.Generate(count);
    }
}