using HeroesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroesAPI.Data
{
    public class HeroesAPIDbContext : DbContext
    {
        public HeroesAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Hero> Heroes { get; set; }
    }
}
