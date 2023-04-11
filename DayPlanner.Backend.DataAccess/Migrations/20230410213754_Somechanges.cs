using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayPlanner.Backend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Somechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2019, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "hmoor@gmail.com", "Henry", "Moor" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
