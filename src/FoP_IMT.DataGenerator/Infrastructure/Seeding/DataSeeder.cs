using FoP_IMT.DataGenerator.Infrastructure.Attributes;
using FoP_IMT.DataGenerator.Services.Erasers;
using FoP_IMT.DataGenerator.Services.Generators;
using System.Reflection;

namespace FoP_IMT.DataGenerator.Infrastructure.Seeding
{
    public class DataSeeder : IDataSeeder
    {
        private readonly IList<IEraser> _erasers;
        private readonly IList<IGenerator> _generators;

        public DataSeeder(
            IEnumerable<IEraser> erasers,
            IEnumerable<IGenerator> generators)
        {
            _erasers = [.. erasers.OrderBy(GetOrder)];
            _generators = [.. generators.OrderBy(GetOrder)];
        }

        private static int GetOrder<T>(T service)
        {
            var orderAttribute = service!.GetType().GetCustomAttribute<OrderAttribute>();
            return orderAttribute?.Order ?? int.MaxValue;
        }

        public async Task SeedDatabase()
        {
            foreach (var eraser in _erasers)
            { await eraser.Erase(); }

            foreach (var generator in _generators)
            { await generator.Generate(); }
        }
    }
}
