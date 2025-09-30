using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaBackendTest.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Seed_Customer_And_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Email", "Name" },
                values: new object[] { new Guid("12081264-5645-407a-ae37-78d5da96fe59"), "Rua Exemplo 1, 123", "cliente1@example.com", "Cliente Exemplo 1" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a612"), "Produto 2" },
                    { new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a683"), "Produto 1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("12081264-5645-407a-ae37-78d5da96fe59"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a612"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a683"));
        }
    }
}
