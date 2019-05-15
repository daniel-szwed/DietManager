namespace DietManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMealModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Ingredients", "Amount", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredients", "Meal_Id", c => c.Int());
            CreateIndex("dbo.Ingredients", "Meal_Id");
            AddForeignKey("dbo.Ingredients", "Meal_Id", "dbo.Meals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredients", "Meal_Id", "dbo.Meals");
            DropIndex("dbo.Ingredients", new[] { "Meal_Id" });
            DropColumn("dbo.Ingredients", "Meal_Id");
            DropColumn("dbo.Ingredients", "Amount");
            DropTable("dbo.Meals");
        }
    }
}
