using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalTrainer.Migrations
{
    public partial class DailyFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductMeal");

            migrationBuilder.DropTable(
                name: "Meal");

            migrationBuilder.CreateTable(
                name: "DailyFoodProduct",
                columns: table => new
                {
                    DailyFoodId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    MealType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyFoodProduct", x => new { x.DailyFoodId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_DailyFoodProduct_DailyFood_DailyFoodId",
                        column: x => x.DailyFoodId,
                        principalTable: "DailyFood",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyFoodProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<Guid>(
                name: "DailyFoodDayId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_DailyFoodDayId",
                table: "Product",
                column: "DailyFoodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyFoodProduct_DailyFoodId",
                table: "DailyFoodProduct",
                column: "DailyFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyFoodProduct_ProductId",
                table: "DailyFoodProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_DailyFood_DailyFoodDayId",
                table: "Product",
                column: "DailyFoodDayId",
                principalTable: "DailyFood",
                principalColumn: "DayId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_DailyFood_DailyFoodDayId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_DailyFoodDayId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DailyFoodDayId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "DailyFoodProduct");

            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    MealId = table.Column<Guid>(nullable: false),
                    DailyFoodDayId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    MealType = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.MealId);
                    table.ForeignKey(
                        name: "FK_Meal_DailyFood_DailyFoodDayId",
                        column: x => x.DailyFoodDayId,
                        principalTable: "DailyFood",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meal_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMeal",
                columns: table => new
                {
                    MealId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMeal", x => new { x.MealId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductMeal_Meal_MealId",
                        column: x => x.MealId,
                        principalTable: "Meal",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductMeal_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meal_DailyFoodDayId",
                table: "Meal",
                column: "DailyFoodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_UserId",
                table: "Meal",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMeal_MealId",
                table: "ProductMeal",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMeal_ProductId",
                table: "ProductMeal",
                column: "ProductId");
        }
    }
}
