namespace Retro.Ad.Domain;

public class Ad : AdDetail
{
    public string CallbackUrl { get; set; }
    public string PayloadBuilderUpsellUrl { get; set; }
}