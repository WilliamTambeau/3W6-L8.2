using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZombieParty.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Weapon",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Weapon",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Zombie",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Zombie",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Zombie",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Zombie",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Zombie",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Zombie",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Zombie",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ZombieType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ZombieType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ZombieType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ZombieType",
                keyColumn: "Id",
                keyValue: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Weapon",
                columns: new[] { "Id", "CreatedDate", "Description", "Force", "Image", "Lifespan", "Name", "Price", "Qty", "QtyBought", "Resistance", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is to cut your friendly zombies", 200m, "https://i.ibb.co/y5nCnXT/dagger-gfce31e967-1920.png", 50m, "Leopold", 421m, 21, 3, 10m, new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An awesome Stainless Steel Shield", 10m, "https://publicdomainvectors.org/photos/Shield-ClassicMedieval1.png", 10m, "Master Shield", 500m, 30, 1, 800m, new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ZombieType",
                columns: new[] { "Id", "Point", "TypeName" },
                values: new object[,]
                {
                    { 1, 1, "Virus" },
                    { 2, 3, "Contact" },
                    { 3, 3, "Fiction" },
                    { 4, 5, "Légendaire" }
                });

            migrationBuilder.InsertData(
                table: "Zombie",
                columns: new[] { "Id", "Force", "Image", "Name", "Point", "ShortDesc", "ZombieTypeId" },
                values: new object[,]
                {
                    { 1, 20, null, "LeChuck", 5, "description", 3 },
                    { 2, 20, null, "Lenore", 4, "description", 3 },
                    { 3, 20, null, "Draugr", 2, "description", 4 },
                    { 4, 20, null, "Ragamuffin", 5, "description", 4 },
                    { 5, 20, null, "Taxidermy", 1, "description", 4 },
                    { 6, 20, null, "chien de l'enfer", 7, "description", 3 },
                    { 7, 20, null, "Avogadro", 9, "description", 3 }
                });
        }
    }
}
