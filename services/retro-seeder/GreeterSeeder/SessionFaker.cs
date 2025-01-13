using Bogus;
using Retro.Domain;

namespace Retro.Seeder.GreeterSeeder;

public class SessionFaker
{
    public static List<Session> GenerateSessions(int count)
    {
        var faker = new Faker<Session>()
            .RuleFor(x => x.UserId, f => f.Random.Guid().ToString())
            .RuleFor(x => x.UserAgent, f => f.Internet.UserAgent())
            .RuleFor(x => x.IpAddress, f => f.Internet.Ip())
            .RuleFor(x => x.Route, f => f.Internet.Url())
            .RuleFor(x => x.EntryEpoch, f => f.Random.Long())
            .RuleFor(x => x.ExitEpoch, f => f.Random.Long())
            .RuleFor(x => x.IsActive, f => f.Random.Bool())
            .RuleFor(x => x.Id, f => f.Random.Guid());

        return faker.Generate(count);
    }
}