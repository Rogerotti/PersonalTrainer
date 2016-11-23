using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalTrainer.Migrations
{
    public partial class test33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fibre",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "TotalFibre",
                table: "DayFoodDiary");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Fibre",
                table: "ProductDetails",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalFibre",
                table: "DayFoodDiary",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
