using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Retro.Auth;

public class GamersDbContext(DbContextOptions<GamersDbContext> options)
    : IdentityDbContext<LoyalGamers, IdentityRole<Guid>, Guid>(options);