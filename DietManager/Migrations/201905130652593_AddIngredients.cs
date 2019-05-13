namespace DietManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIngredients : DbMigration
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
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ingredients");
        }
    }
}
