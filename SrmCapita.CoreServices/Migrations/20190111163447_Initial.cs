using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SrmCapita.CoreServices.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Fone = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Limite = table.Column<decimal>(type: "decimal", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
