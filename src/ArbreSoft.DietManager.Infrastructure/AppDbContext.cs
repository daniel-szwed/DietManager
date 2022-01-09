using ArbreSoft.DietManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace ArbreSoft.DietManager.Infrastructure
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Filename=DietManager.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NutritionFact>()
                .HasMany(x => x.Childrens)
                .WithOne().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NutritionFact>()
                .ToTable("NutritionFacts")
                .HasDiscriminator<string>("Type")
                .HasValue<Ingredient>(nameof(Ingredient))
                .HasValue<Meal>(nameof(Meal))
                .HasValue<DailyMenu>(nameof(DailyMenu))
                .HasValue<Diet>(nameof(Diet));
            //modelBuilder.SetRequiredProperties<Ingredient>().Entity<Ingredient>().ToTable("Ingredients");
            //modelBuilder.SetRequiredProperties<Meal>().Entity<Meal>().ToTable("Meals");
            //modelBuilder.SetRequiredProperties<DailyMenu>().Entity<DailyMenu>().ToTable("DailyMenu");
            //modelBuilder.SetRequiredProperties<Diet>().Entity<Diet>().ToTable("Diets");
        }

        public void Migrate()
        {
            Database.Migrate();
        }

        public virtual DbSet<NutritionFact> NutritionFacts { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<DailyMenu> DailyMenu { get; set; }
        public virtual DbSet<Diet> Diets { get; set; }
    }

    public static class Extensions
    {
        public static ModelBuilder SetRequiredProperties<T>(this ModelBuilder modelBuilder) where T : NutritionFact
        {
            modelBuilder.Entity<T>().Property(x => x.Name).IsRequired();

            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.PropertyType == typeof(decimal))
                {
                    modelBuilder.Entity<T>().Property(prop.Name).IsRequired();
                }
            }

            return modelBuilder;
        }
    }
}
