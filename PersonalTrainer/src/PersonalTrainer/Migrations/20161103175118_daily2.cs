using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalTrainer.Migrations
{
    public partial class daily2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    HashCode = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DailyFood",
                columns: table => new
                {
                    DailyFoodId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    TotalCalories = table.Column<decimal>(nullable: false),
                    TotalCarbohydrates = table.Column<decimal>(nullable: false),
                    TotalFat = table.Column<decimal>(nullable: false),
                    TotalFibre = table.Column<decimal>(nullable: false),
                    TotalProteins = table.Column<decimal>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyFood", x => x.DailyFoodId);
                    table.ForeignKey(
                        name: "FK_DailyFood_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    ProductState = table.Column<int>(nullable: false),
                    ProductType = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Height = table.Column<decimal>(nullable: false),
                    HeightUnit = table.Column<int>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserDetails_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyFoodProduct",
                columns: table => new
                {
                    DailyFoodId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    MealType = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyFoodProduct", x => new { x.DailyFoodId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_DailyFoodProduct_DailyFood_DailyFoodId",
                        column: x => x.DailyFoodId,
                        principalTable: "DailyFood",
                        principalColumn: "DailyFoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyFoodProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    Calories = table.Column<decimal>(nullable: false),
                    Carbohydrates = table.Column<decimal>(nullable: false),
                    Fat = table.Column<decimal>(nullable: false),
                    Fibre = table.Column<decimal>(nullable: false),
                    Protein = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    QuantityType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyFood_UserId",
                table: "DailyFood",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyFoodProduct_DailyFoodId",
                table: "DailyFoodProduct",
                column: "DailyFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyFoodProduct_ProductId",
                table: "DailyFoodProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UserId",
                table: "Product",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserId",
                table: "UserDetails",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyFoodProduct");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "DailyFood");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
