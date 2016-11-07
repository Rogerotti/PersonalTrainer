using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalTrainer.Migrations
{
    public partial class Goals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGoal",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    BodyFat = table.Column<decimal>(nullable: false),
                    Calories = table.Column<int>(nullable: false),
                    Carbohydrates = table.Column<decimal>(nullable: false),
                    Fat = table.Column<decimal>(nullable: false),
                    Fibre = table.Column<decimal>(nullable: false),
                    Proteins = table.Column<decimal>(nullable: false),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGoal", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserGoal_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGoal_UserId",
                table: "UserGoal",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGoal");
        }
    }
}
