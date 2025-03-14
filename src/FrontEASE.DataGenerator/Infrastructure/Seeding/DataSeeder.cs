﻿using FrontEASE.DataGenerator.Infrastructure.Attributes;
using FrontEASE.DataGenerator.Services.Erasers;
using FrontEASE.DataGenerator.Services.Generators;
using System.Reflection;

namespace FrontEASE.DataGenerator.Infrastructure.Seeding
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
