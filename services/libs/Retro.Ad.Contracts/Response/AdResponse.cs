namespace Retro.Ad.Contracts.Response;

public record AdResponse{
    public Guid Id {get; init;}
    public string? Title {get; init;}
    public string? FullDescription {get; init;}
    public string? ShortDescription {get; init;}
    public string? ImageUrl {get; init;}
    public DateTimeOffset? StartDateTime {get; init;}
    public DateTimeOffset? EndDateTime {get; init;}
    public bool? IsActive {get; init;}
    public bool? IsFeatured {get; init;}
    public string? RenderedHtml {get; init;}
    public string CallbackUrl {get; init;} = string.Empty;
    public string PayloadBuilderUpsellUrl {get; init;} = string.Empty;
    
    public AdResponse() { }
    
    public AdResponse(Domain.Ad ad)
    {
        Id = ad.Id;
        Title = ad.Title;
        FullDescription = ad.FullDescription;
        ShortDescription = ad.ShortDescription;
        ImageUrl = ad.ImageUrl;
        StartDateTime = ad.StartDateTime;
        EndDateTime = ad.EndDateTime;
        IsActive = ad.IsActive;
        IsFeatured = ad.IsFeatured;
        RenderedHtml = ad.RenderedHtml;
        CallbackUrl = ad.CallbackUrl;
        PayloadBuilderUpsellUrl = ad.PayloadBuilderUpsellUrl;
    }
}
