namespace Retro.Ad.Contracts.Request;

public record UpsertAdRequest(Guid? Id,
    string Title,
    string? FullDescription,
    string? ShortDescription,
    string? ImageUrl,
    DateTimeOffset StartDateTime,
    DateTimeOffset EndDateTime,
    bool IsActive,
    bool IsFeatured,
    string? RenderedHtml,
    string CreatedBy,
    DateTimeOffset CreatedDateTime,
    string? UpdatedBy,
    DateTimeOffset? UpdatedDateTime);
