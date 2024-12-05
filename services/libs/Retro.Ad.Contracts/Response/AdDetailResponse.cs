namespace Retro.Ad.Contracts.Response;

public record AdDetailResponse(
    Guid Id,
    string Title,
    string FullDescription,
    string ShortDescription,
    string ImageUrl,
    DateTimeOffset StartDateTime,
    DateTimeOffset EndDateTime,
    bool IsActive,
    bool IsFeatured,
    string RenderedHtml,
    string CreatedBy,
    DateTimeOffset CreatedDateTime,
    string UpdatedBy,
    DateTimeOffset UpdatedDateTime);