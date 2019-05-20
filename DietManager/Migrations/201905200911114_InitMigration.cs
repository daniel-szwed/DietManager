namespace DietManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
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
                        Meal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meals", t => t.Meal_Id)
                .Index(t => t.Meal_Id);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredients", "Meal_Id", "dbo.Meals");
            DropIndex("dbo.Ingredients", new[] { "Meal_Id" });
            DropTable("dbo.Meals");
            DropTable("dbo.Ingredients");
        }
    }
}
