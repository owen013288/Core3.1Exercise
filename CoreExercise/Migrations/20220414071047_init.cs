using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreExercise.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "SalesReport",
                columns: table => new
                {
                    SalesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(maxLength: 20, nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesVolume = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReport", x => x.SalesId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SalesReport");
        }
    }
}
