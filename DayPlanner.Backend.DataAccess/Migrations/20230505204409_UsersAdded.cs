using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayPlanner.Backend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UsersAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SaltHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SaltHash",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2019, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "hmoor@gmail.com", "Henry", "Moor" });

            migrationBuilder.InsertData(
                table: "TaskItems",
                columns: new[] { "Id", "BoardId", "CreatedAt", "CreatorId", "DueDate", "Text" },
                values: new object[] { 530, 0, new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jog 5km" });
        }
    }
}
