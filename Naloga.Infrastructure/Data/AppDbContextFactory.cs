using Microsoft.EntityFrameworkCore;

namespace Naloga.Infrastructure.Data
{
    public static class AppDbContextFactory
    {
        public static void AddInMemoryDatabase(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb"));
        }
    }
}
