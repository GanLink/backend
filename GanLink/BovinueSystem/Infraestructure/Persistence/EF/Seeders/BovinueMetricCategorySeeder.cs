using GanLink.BovinueSystem.Domain.Models.Aggregates;

namespace GanLink.BovinueSystem.Infraestructure.Persistence.EF.Seeders
{
    public static class BovinueMetricCategorySeeder
    {
        public static IEnumerable<BovinueMetricCategory> GetData()
        {
            return new List<BovinueMetricCategory>
            {
                new() { Id = 1, Category = "Productive (Milk)" },
                new() { Id = 2, Category = "Productive (Meat)" },
                new() { Id = 3, Category = "Feed Efficiency" },
                new() { Id = 4, Category = "Reproductive/Pregnancy" },
                new() { Id = 5, Category = "Genetic" },
                new() { Id = 6, Category = "Sanitary" }
            };
        }
    }
}