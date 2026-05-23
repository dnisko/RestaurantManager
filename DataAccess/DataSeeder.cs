using DomainModels;

namespace DataAccess
{
    public static class DataSeeder
    {
        public static void SeedData(RestaurantDbContext context)
        {
            SeedCategories(context);
            SeedDiningTables(context);
        }

        private static void SeedCategories(RestaurantDbContext context)
        {
            if (context.Categories.Any()) return;

            var categories = new List<Category>
            {
                new() { Name = "Hot Drinks" },
                new() { Name = "Cold Drinks" },
                new() { Name = "Food" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();
            Console.WriteLine("✅ Categories seeded.");
        }

        private static void SeedDiningTables(RestaurantDbContext context)
        {
            if (context.RestaurantTables.Any()) return;

            var tables = new List<RestaurantTable>();

            for (int i = 1; i <= 10; i++)
            {
                tables.Add(new RestaurantTable
                {
                    Name = $"Table {i}",
                    IsOccupied = false
                });
            }

            context.RestaurantTables.AddRange(tables);
            context.SaveChanges();
            Console.WriteLine("✅ Dining tables seeded.");
        }
    }
}