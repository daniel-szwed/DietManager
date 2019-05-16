namespace DietManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IngredientBases",
                c => new
                    {
                        IngredientBaseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Kcal = c.Single(nullable: false),
                        Protein = c.Single(nullable: false),
                        Carbohydrates = c.Single(nullable: false),
                        Sugar = c.Single(nullable: false),
                        Fat = c.Single(nullable: false),
                        Saturated = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientBaseId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Kcal = c.Single(nullable: false),
                        Protein = c.Single(nullable: false),
                        Carbohydrates = c.Single(nullable: false),
                        Sugar = c.Single(nullable: false),
                        Fat = c.Single(nullable: false),
                        Saturated = c.Single(nullable: false),
                        Amount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientId);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MealIngredients",
                c => new
                    {
                        Meal_Id = c.Int(nullable: false),
                        Ingredient_IngredientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_Id, t.Ingredient_IngredientId })
                .ForeignKey("dbo.Meals", t => t.Meal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_IngredientId, cascadeDelete: true)
                .Index(t => t.Meal_Id)
                .Index(t => t.Ingredient_IngredientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MealIngredients", "Ingredient_IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.MealIngredients", "Meal_Id", "dbo.Meals");
            DropIndex("dbo.MealIngredients", new[] { "Ingredient_IngredientId" });
            DropIndex("dbo.MealIngredients", new[] { "Meal_Id" });
            DropTable("dbo.MealIngredients");
            DropTable("dbo.Meals");
            DropTable("dbo.Ingredients");
            DropTable("dbo.IngredientBases");
        }
    }
}
