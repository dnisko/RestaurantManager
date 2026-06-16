using DomainModels;
using DomainModels.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientPrice> IngredientsPrice { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RecipeLine> RecipeLines { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<RestaurantTable> RestaurantTables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(ISoftDeletable).IsAssignableFrom(t.ClrType)))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var filter = Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted)),
                        Expression.Constant(false)),
                    parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }

            modelBuilder.Entity<IngredientPrice>()
                .Property(ip => ip.Price)
                .HasPrecision(18, 6);

            modelBuilder.Entity<RecipeLine>()
                .Property(rl => rl.Quantity)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Ingredient>()
                .Property(i => i.QuantityOnHand)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Ingredient>()
                .Property(i => i.MinimumQuantity)
                .HasPrecision(18, 4);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
