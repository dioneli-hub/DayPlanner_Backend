using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayPlanner.Backend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Somechanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaskItems",
                columns: new[] { "Id", "BoardId", "CreatedAt", "CreatorId", "DueDate", "Text" },
                values: new object[] { 530, 0, new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jog 5km" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 530);
        }
    }
}
