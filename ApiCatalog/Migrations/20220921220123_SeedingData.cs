using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalog.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "ImageUrl", "Name" },
                values: new object[] { 1, "comidas.jpg", "Comidas" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "ImageUrl", "Name" },
                values: new object[] { 2, "bebidas.jpg", "Bebidas" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "ImageUrl", "Name" },
                values: new object[] { 3, "sobremesas.jpg", "Sobremesas" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Date", "Description", "ImageUrl", "Inventory", "Name", "Price" },
                values: new object[] { 1, 1, new DateTime(2022, 9, 21, 19, 1, 23, 511, DateTimeKind.Local).AddTicks(3618), "Lasanha congelada", "lasanha.jpg", 25, "Lasanha", 15.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Date", "Description", "ImageUrl", "Inventory", "Name", "Price" },
                values: new object[] { 2, 2, new DateTime(2022, 9, 21, 19, 1, 23, 511, DateTimeKind.Local).AddTicks(3629), "Refrigerante Guaraná 2L", "guarana.jpg", 54, "Guaraná", 7.5 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Date", "Description", "ImageUrl", "Inventory", "Name", "Price" },
                values: new object[] { 3, 3, new DateTime(2022, 9, 21, 19, 1, 23, 511, DateTimeKind.Local).AddTicks(3630), "Sobremesa deliciosa. Acompanha sorvete", "petit-gateau.jpg", 17, "Petit Gateau", 20.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);
        }
    }
}
