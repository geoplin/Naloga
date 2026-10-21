using Microsoft.EntityFrameworkCore;
using Naloga.Domain.Entities;

namespace Naloga.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Order> Orders { get; set; }
    }
}
