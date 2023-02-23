using LinkShortener.Api.Models;
using LinkShortener.Api.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Api.Data.DataBase;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> ops) : base(ops)
    {
        
    }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<ShortenLinkModel> Links { get; set; }
}