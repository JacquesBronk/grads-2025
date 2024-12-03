namespace Retro.Ads.Admin.Domain.Model;

public class AdEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string FullDescriptionm { get; set; }
    public string ShortDescription { get; set; }
    public string ImageUrl { get; set; }
    public DateTimeOffset StartDateTime { get; set; }
    public DateTimeOffset EndDateTime { get; set; }
    public bool IsActive { get; set; }
    
}