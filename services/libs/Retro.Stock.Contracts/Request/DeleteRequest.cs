namespace Retro.Stock.Contracts.Request;

public record DeleteRequest(Guid? Id, string? Sku);
