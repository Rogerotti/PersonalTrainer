using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalTrainer.Migrations
{
    public partial class mig02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiaryProduct",
                table: "DiaryProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiaryProduct",
                table: "DiaryProduct",
                column: "DiaryProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiaryProduct",
                table: "DiaryProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiaryProduct",
                table: "DiaryProduct",
                columns: new[] { "DiaryProductId", "DayId", "ProductId" });
        }
    }
}
