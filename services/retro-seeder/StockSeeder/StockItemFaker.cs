using Bogus;
using Retro.Stock.Domain;

namespace Retro.Seeder.StockSeeder;

public static class StockItemFaker
{
    public static List<StockItem> GenerateStockItems(int count)
    {
        var faker = new Faker<StockItem>()
            .RuleFor(s => s.Id, f => Guid.NewGuid())
            .RuleFor(s => s.Sku, f => f.Commerce.Ean13())
            .RuleFor(s => s.Title, f => f.Commerce.ProductName())
            .RuleFor(s => s.Description, f => f.Lorem.Paragraph())
            .RuleFor(s => s.ImageUrl, f => f.Image.PicsumUrl())
            .RuleFor(s => s.Condition, f => f.PickRandom<StockCondition>())
            .RuleFor(s => s.Quantity, f => f.Random.Int(0, 100))
            .RuleFor(s => s.Price, f => f.Random.Decimal(1, 1000))
            .RuleFor(s => s.IsDiscounted, f => f.Random.Bool())
            .RuleFor(s => s.DiscountPercentage, (f, s) => s.IsDiscounted ? f.Random.Double(5, 50) : 0)
            .RuleFor(s => s.CreatedAt, f => f.Date.PastOffset(2))
            .RuleFor(s => s.UpdatedAt, (f, s) => s.CreatedAt.AddDays(f.Random.Int(1, 100)))
            .RuleFor(s => s.CreatedBy, f => f.Person.FullName)
            .RuleFor(s => s.UpdatedBy, f => f.Person.FullName)
            .RuleFor(s => s.Tags, f => f.Commerce.Categories(3).ToArray());

        return faker.Generate(count);
    }
}