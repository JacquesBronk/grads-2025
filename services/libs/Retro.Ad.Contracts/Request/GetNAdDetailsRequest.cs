namespace Retro.Ad.Contracts.Request;

public record GetNAdDetailsRequest(int Number, int PageSize = 10, int PageNumber = 1);