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
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Kcal = c.Single(nullable: false),
                        Protein = c.Single(nullable: false),
                        Carbohydrates = c.Single(nullable: false),
                        Sugar = c.Single(nullable: false),
                        Fat = c.Single(nullable: false),
                        Saturated = c.Single(nullable: false),
                        Amount = c.Single(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Ingredient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_Id, t.Ingredient_Id })
                .ForeignKey("dbo.Meals", t => t.Meal_Id, cascadeDelete: true)
                .ForeignKey("dbo.IngredientBases", t => t.Ingredient_Id, cascadeDelete: true)
                .Index(t => t.Meal_Id)
                .Index(t => t.Ingredient_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MealIngredients", "Ingredient_Id", "dbo.IngredientBases");
            DropForeignKey("dbo.MealIngredients", "Meal_Id", "dbo.Meals");
            DropIndex("dbo.MealIngredients", new[] { "Ingredient_Id" });
            DropIndex("dbo.MealIngredients", new[] { "Meal_Id" });
            DropTable("dbo.MealIngredients");
            DropTable("dbo.Meals");
            DropTable("dbo.IngredientBases");
        }
    }
}
