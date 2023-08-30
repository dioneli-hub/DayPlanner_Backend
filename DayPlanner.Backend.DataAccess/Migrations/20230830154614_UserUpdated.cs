using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DayPlanner.Backend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecurringPatterns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    RecurringType = table.Column<int>(type: "int", nullable: false),
                    OccurencesNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringPatterns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardMembers",
                columns: table => new
                {
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardMembers", x => new { x.BoardId, x.MemberId });
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaltHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswrodTokenExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    BoardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    ChangeRecurredChildren = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    PerformerId = table.Column<int>(type: "int", nullable: true),
                    IsOverdue = table.Column<bool>(type: "bit", nullable: false),
                    ParentTaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItems_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskItems_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskItems_Users_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BoardId", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "ResetPasswordToken", "ResetPasswrodTokenExpiresAt", "SaltHash", "VerificationToken", "VerifiedAt" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Dioneli@mail.ru1", "Madison", "Walker", "x/5fpi8JiMGXxM4Re4fzlamU61mQQMGNR50wxtwCaHw=", null, null, "mlJyHV/cYHAT2ErFkB8d5w==", "bCGM/xNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==", new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 2, null, new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "D1!q2222@ru", "Sam", "McGregor", "FBHiJLzMEWDHoMgTd1rqQQbDaucEQStWzFba3FRL54I=", null, null, "FyQp6hr65+F7jI0btRXMLw==", "OCGOOxNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==", new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 3, null, new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "vikdim@gmail.com", "Viktor", "Dimashevski", "FBHiJLzMEWDHoMgTd1rqQQbDaucEQStWzFba3FRL54I=", null, null, "FyQp6hr65+F7jI0btRXMLw==", "bCGM/xNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==", new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 4, null, new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "liamwall@gmail.com", "Liam", "Wall", "FBHiJLzMEWDHoMgTd1rqQQbDaucEQStWzFba3FRL54I=", null, null, "FyQp6hr65+F7jI0btRXMLw==", "bCGM/xNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==", new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 5, null, new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "kktailor@gmail.com", "Karen", "Tailor", "FBHiJLzMEWDHoMgTd1rqQQbDaucEQStWzFba3FRL54I=", null, null, "FyQp6hr65+F7jI0btRXMLw==", "bCGM/xNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==", new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardMembers_MemberId",
                table: "BoardMembers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_CreatorId",
                table: "Boards",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_BoardId",
                table: "TaskItems",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_CreatorId",
                table: "TaskItems",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_PerformerId",
                table: "TaskItems",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BoardId",
                table: "Users",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardMembers_Boards_BoardId",
                table: "BoardMembers",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardMembers_Users_MemberId",
                table: "BoardMembers",
                column: "MemberId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Users_CreatorId",
                table: "Boards",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Boards_BoardId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "BoardMembers");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RecurringPatterns");

            migrationBuilder.DropTable(
                name: "TaskItems");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
