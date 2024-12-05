using Bogus;
using Retro.Ad.Domain;

namespace Retro.Seeder.AdSeeder;

public static class AdDetailFaker
{
    public static List<AdDetail> GenerateAdDetails(int count)
    {
        var faker = new Faker<AdDetail>()
            .RuleFor(a => a.Id, _ => Guid.NewGuid())
            .RuleFor(a => a.Title, f => f.Commerce.ProductName())
            .RuleFor(a => a.FullDescription, f => f.Lorem.Paragraph())
            .RuleFor(a => a.ShortDescription, f => f.Lorem.Sentence())
            .RuleFor(a => a.ImageUrl, f => f.Image.PicsumUrl())
            .RuleFor(a => a.IsActive, f => f.Random.Bool())
            .RuleFor(a => a.StartDateTime, f => f.Date.FutureOffset())
            .RuleFor(a => a.IsFeatured, f => f.Random.Bool())
            .RuleFor(a => a.RenderedHtml, f => FakeHtmlTemplateBuilder(f.Commerce.ProductName(), f.Lorem.Sentence(), f.Image.PicsumUrl()))
            .RuleFor(a => a.CreatedBy, f => f.Person.FullName)
            .RuleFor(a => a.CreatedDateTime, f => f.Date.PastOffset(2))
            .RuleFor(a => a.UpdatedBy, f => f.Person.FullName)
            .RuleFor(a => a.UpdatedDateTime, (f, a) => a.CreatedDateTime.AddDays(f.Random.Int(1, 100)))
            .RuleFor(a => a.EndDateTime, (f, a) => a.StartDateTime.AddDays(f.Random.Int(1, 100)));

        return faker.Generate(count);
    }
    
    private static string FakeHtmlTemplateBuilder(string title, string description, string imageUrl)
    {
        return $"<div><h1>{title}</h1><p>{description}</p><img src=\"{imageUrl}\" alt=\"{title}\" /></div>";
    }
}