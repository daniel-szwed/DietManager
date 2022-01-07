using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArbreSoft.DietManager.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NutritionFacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    KiloCalories = table.Column<decimal>(type: "TEXT", nullable: false),
                    Proteins = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalCarbohydreates = table.Column<decimal>(type: "TEXT", nullable: false),
                    Sugars = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalFats = table.Column<decimal>(type: "TEXT", nullable: false),
                    SaturatedFats = table.Column<decimal>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Weight = table.Column<decimal>(type: "TEXT", nullable: true),
                    NutritionFactId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Time = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionFacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionFacts_NutritionFacts_NutritionFactId",
                        column: x => x.NutritionFactId,
                        principalTable: "NutritionFacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NutritionFacts_NutritionFactId",
                table: "NutritionFacts",
                column: "NutritionFactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionFacts");
        }
    }
}
