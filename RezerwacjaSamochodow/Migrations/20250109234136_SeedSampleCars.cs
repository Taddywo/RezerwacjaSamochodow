using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RezerwacjaSamochodow.Migrations
{
    /// <inheritdoc />
    public partial class SeedSampleCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Model", "PricePerDay", "Year" },
                values: new object[,]
                {
                    { 1, "Toyota", "Corolla", 50.00m, 2020 },
                    { 2, "Ford", "Focus", 45.00m, 2019 },
                    { 3, "BMW", "3 Series", 80.00m, 2021 },
                    { 4, "Tesla", "Model 3", 100.00m, 2022 },
                    { 5, "Honda", "Civic", 40.00m, 2018 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
