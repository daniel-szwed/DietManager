namespace DietManager.DataLayer
{
    using DietManager.Migrations;
    using DietManager.Models;
    using System.Data.Entity;

    public class AppDbContext : DbContext
    {
        // Your context has been configured to use a 'AppDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DietManager.Services.AppDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AppDbContext' 
        // connection string in the application configuration file.
        public AppDbContext()
            : base("name=AppDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        public virtual DbSet<IngredientBase> IngredientBase { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
    }
}