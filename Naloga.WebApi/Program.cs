using Naloga.Aplication.Interfaces;
using Naloga.Aplication.Services;
using Naloga.Domain.Entities;
using Naloga.Infrastructure.Data;
using Naloga.Infrastructure.Repositories;
using Naloga.WebApi.Filters;
using Scalar.AspNetCore;

public class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

        // Add services to the container.
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<GlobalExceptionFilter>();
        });

        builder.Services.AddOpenApi();

        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();

        if (useInMemoryDatabase)
        {
            builder.Services.AddInMemoryDatabase();
        }
        else
        {
            // Connect to a database server
        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // Seed the database with sample data from a JSON file if using in-memory database
        if (useInMemoryDatabase)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                var adder = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                SeedData(context, logger, adder);
            }
        }

        app.Run();
    }

    private static void SeedData(AppDbContext context, ILogger<Program> logger, IOrderRepository adder)
    {
        // Check if the database is empty
        if (!context.Orders.Any())
        {
            var filename = "mock_data.json";
            var jsonString = File.ReadAllText(filename);
            var orders = System.Text.Json.JsonSerializer.Deserialize<List<Order>>(jsonString);
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    adder.AddOrder(order);
                }
                context.SaveChanges();
                logger.LogInformation($"Database seeded with sample data from {filename}");
            }
        }
    }
}