using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nologo.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Author = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    RecipeFileName = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Instructions = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Ingredients = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddedBy = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
