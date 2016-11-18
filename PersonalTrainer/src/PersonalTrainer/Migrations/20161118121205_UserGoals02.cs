using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalTrainer.Migrations
{
    public partial class UserGoals02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserGoal_UserId",
                table: "UserGoal");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_UserId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "UserGoal");

            migrationBuilder.RenameColumn(
                name: "Fibre",
                table: "UserGoal",
                newName: "PercentageProtein");

            migrationBuilder.RenameColumn(
                name: "BodyFat",
                table: "UserGoal",
                newName: "PercentageFat");

            migrationBuilder.AlterColumn<int>(
                name: "Proteins",
                table: "UserGoal",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Fat",
                table: "UserGoal",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Carbohydrates",
                table: "UserGoal",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<decimal>(
                name: "PercentageCarbs",
                table: "UserGoal",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentageCarbs",
                table: "UserGoal");

            migrationBuilder.RenameColumn(
                name: "PercentageProtein",
                table: "UserGoal",
                newName: "Fibre");

            migrationBuilder.RenameColumn(
                name: "PercentageFat",
                table: "UserGoal",
                newName: "BodyFat");

            migrationBuilder.AlterColumn<decimal>(
                name: "Proteins",
                table: "UserGoal",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Fat",
                table: "UserGoal",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Carbohydrates",
                table: "UserGoal",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "UserGoal",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserGoal_UserId",
                table: "UserGoal",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserId",
                table: "UserDetails",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId",
                unique: true);
        }
    }
}
