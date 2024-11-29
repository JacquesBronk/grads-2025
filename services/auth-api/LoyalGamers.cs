using Microsoft.AspNetCore.Identity;

namespace Retro.Auth;

public class LoyalGamers : IdentityUser<Guid>
{
    public DateTimeOffset LastSeen { get; set; } = DateTimeOffset.UtcNow;
    public int LoyaltyPoints { get; set; } = 0;
    public string? FavoriteGame { get; set; }
    public string? FavoriteConsole { get; set; }
    public string? FavoriteGenre { get; set; }
    public string? FavoriteDeveloper { get; set; }
    public string? FavoritePublisher { get; set; }
    public string? FavoritePlatform { get; set; }
    public string? FavoriteEngine { get; set; }
    public string? FavoriteCharacter { get; set; }
    public string? FavoriteLocation { get; set; }
    public string? FavoriteItem { get; set; }
    public string? FavoriteWeapon { get; set; }
    public string? FavoriteVehicle { get; set; }
    
}