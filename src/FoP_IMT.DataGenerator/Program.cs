using FoP_IMT.DataGenerator;
using FoP_IMT.DataGenerator.Infrastructure.Seeding;
using FoP_IMT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


await using (var factory = new DataGeneratorApp())
{
    using var scope = factory.Services.CreateScope();
    try
    {
        await using var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // Apply migrations
        await dataContext.Database.MigrateAsync();

        // Seed Data
        var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
        await dataSeeder.SeedDatabase();

    }
    catch (Exception ex)
    {
        await Console.Out.WriteLineAsync($"An error occurred: {ex.Message}");
    }
}