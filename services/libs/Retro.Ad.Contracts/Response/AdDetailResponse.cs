namespace Retro.Ad.Contracts.Response;

public record AdDetailResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string FullDescription { get; set; }
    public string ShortDescription { get; set; }
    public string ImageUrl { get; set; }
    public DateTimeOffset StartDateTime { get; set; }
    public DateTimeOffset EndDateTime { get; set; }
    public bool IsActive { get; set; }
    public bool IsFeatured { get; set; }
    public string RenderedHtml { get; set; }
    public string CreatedBy { get; set; } 
    public DateTimeOffset CreatedDateTime { get; set; }
    public string UpdatedBy { get; set; }
    public DateTimeOffset UpdatedDateTime { get; set; }
    
    public AdDetailResponse() { }
    
    public AdDetailResponse(Guid id, string title, string fullDescription, string shortDescription, string imageUrl, DateTimeOffset startDateTime, DateTimeOffset endDateTime, bool isActive, bool isFeatured, string renderedHtml, string createdBy, DateTimeOffset createdDateTime, string updatedBy, DateTimeOffset updatedDateTime)
    {
        Id = id;
        Title = title;
        FullDescription = fullDescription;
        ShortDescription = shortDescription;
        ImageUrl = imageUrl;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        IsActive = isActive;
        IsFeatured = isFeatured;
        RenderedHtml = renderedHtml;
        CreatedBy = createdBy;
        CreatedDateTime = createdDateTime;
        UpdatedBy = updatedBy;
        UpdatedDateTime = updatedDateTime;
    }
};