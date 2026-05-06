using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarAndPreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "StatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "StatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "StatusId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "StatusId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPriority",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserNotificationPreferences",
                columns: table => new
                {
                    PreferenceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    IsPriority = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotificationPreferences", x => x.PreferenceId);
                    table.ForeignKey(
                        name: "FK_UserNotificationPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationPreferences_UserId",
                table: "UserNotificationPreferences",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserNotificationPreferences");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsPriority",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Notifications");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Manager" },
                    { 3, "Employee" }
                });

            migrationBuilder.InsertData(
                table: "TaskStatuses",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "To Do" },
                    { 2, "In Progress" },
                    { 3, "Completed" },
                    { 4, "Overdue" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "FullName", "IsDeleted", "PasswordHash", "RoleId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 3, 16, 32, 43, 395, DateTimeKind.Local).AddTicks(8776), "admin1@task.com", "Admin One", false, "$2a$11$i4CsdatACSA5qUHGLfUAreXQSdNS5lspvbXxAM0OACDWd3jQjhSua", 1 },
                    { 2, new DateTime(2026, 5, 3, 16, 32, 43, 716, DateTimeKind.Local).AddTicks(1350), "admin2@task.com", "Admin Two", false, "$2a$11$StoFHPynkkO4/HBJrMtcheZxfPSgNN1c36oYa8.MUQZz81LZM/jr.", 1 },
                    { 3, new DateTime(2026, 5, 3, 16, 32, 44, 21, DateTimeKind.Local).AddTicks(5582), "manager1@task.com", "Manager 1", false, "$2a$11$uZxa.VM1vibCo0nLBAVZBe7ZsOnrAYQZO4OZmtMPHEelU1wPuJ/iK", 2 },
                    { 4, new DateTime(2026, 5, 3, 16, 32, 44, 332, DateTimeKind.Local).AddTicks(9032), "manager2@task.com", "Manager 2", false, "$2a$11$JedbqFYrW3X3Cy8CoXwCSenomUQd57byd/dbkKUDt7tQk5OgExkCK", 2 },
                    { 5, new DateTime(2026, 5, 3, 16, 32, 44, 645, DateTimeKind.Local).AddTicks(6986), "manager3@task.com", "Manager 3", false, "$2a$11$SNC1jhiekkDUVgG8l/ulT.YQosp/Z48CnBT1u9nCBgsC8idBsRzZa", 2 },
                    { 6, new DateTime(2026, 5, 3, 16, 32, 44, 941, DateTimeKind.Local).AddTicks(2564), "manager4@task.com", "Manager 4", false, "$2a$11$TRqYvd/eYx0izyRyTIOor.NlKiZNWbYGX.bKhM/IwqDz8CjM3VTAC", 2 },
                    { 7, new DateTime(2026, 5, 3, 16, 32, 45, 242, DateTimeKind.Local).AddTicks(8116), "manager5@task.com", "Manager 5", false, "$2a$11$53Itim/EtYkAHCxtntGfFeaEb25x19Pum8RfGsyX/FAU2ftd/cJcu", 2 },
                    { 8, new DateTime(2026, 5, 3, 16, 32, 45, 553, DateTimeKind.Local).AddTicks(6667), "employee1@task.com", "Employee 1", false, "$2a$11$tlcJFpn0apgzsM2Di1mo2OBNwokG3trreitgbtT9B53wWesEvQV0u", 3 },
                    { 9, new DateTime(2026, 5, 3, 16, 32, 45, 854, DateTimeKind.Local).AddTicks(7487), "employee2@task.com", "Employee 2", false, "$2a$11$LxhFh0L7D0bmq6y0P8htjeghoKCMy3oWFgAlf7oUZf4ZAsdshY6lu", 3 },
                    { 10, new DateTime(2026, 5, 3, 16, 32, 46, 144, DateTimeKind.Local).AddTicks(1611), "employee3@task.com", "Employee 3", false, "$2a$11$nUlTot1GoIX4oScKpwnzwObuJNcxUqi8BGIzRHDjtKpbbDW5Lu5FW", 3 },
                    { 11, new DateTime(2026, 5, 3, 16, 32, 46, 418, DateTimeKind.Local).AddTicks(5405), "employee4@task.com", "Employee 4", false, "$2a$11$Am3O7kay/4KRwFqCg7Iv8.FZo7hZ37ku6kMmet74giGuoOq9KeiAa", 3 },
                    { 12, new DateTime(2026, 5, 3, 16, 32, 46, 700, DateTimeKind.Local).AddTicks(5686), "employee5@task.com", "Employee 5", false, "$2a$11$N/d7ma.KbF8BVsEgZ1wyqOxZZ3hT/pL6XHFShbcmlhAIJVxn0A7KK", 3 },
                    { 13, new DateTime(2026, 5, 3, 16, 32, 46, 955, DateTimeKind.Local).AddTicks(9967), "employee6@task.com", "Employee 6", false, "$2a$11$GmNeWfmkiVJltVAJJ5ay3.4kCyKyr6YEGKAyq297ajcCPTOEHiaWe", 3 },
                    { 14, new DateTime(2026, 5, 3, 16, 32, 47, 261, DateTimeKind.Local).AddTicks(7667), "employee7@task.com", "Employee 7", false, "$2a$11$nKdfss/KSPbxWG1nTaPjb.9bvMRQXnaBXxkBjQFc5U.WECmhqYH5W", 3 },
                    { 15, new DateTime(2026, 5, 3, 16, 32, 47, 785, DateTimeKind.Local).AddTicks(5394), "employee8@task.com", "Employee 8", false, "$2a$11$ouLXSWrfmJlRs8kUi4OJtuWv7T0rfVfhX7ZQiHcQDwTj4D6d8y/NS", 3 },
                    { 16, new DateTime(2026, 5, 3, 16, 32, 48, 71, DateTimeKind.Local).AddTicks(5113), "employee9@task.com", "Employee 9", false, "$2a$11$uQ.0PpF5J/xLUL04ZXccueExelH5ENYtGENiW3uYLNm..o7D/xrkW", 3 },
                    { 17, new DateTime(2026, 5, 3, 16, 32, 48, 387, DateTimeKind.Local).AddTicks(6832), "employee10@task.com", "Employee 10", false, "$2a$11$7RsB9QSCLqIkWgoC0UthM.At1oEVrm9vttDxlfaleMy8ow7d9TBYK", 3 },
                    { 18, new DateTime(2026, 5, 3, 16, 32, 48, 695, DateTimeKind.Local).AddTicks(8996), "employee11@task.com", "Employee 11", false, "$2a$11$Rscdm7/YkCuht7JnI4YM4O0FVtXgEXdq5i24e.yjibX3t.U.kPvRu", 3 },
                    { 19, new DateTime(2026, 5, 3, 16, 32, 48, 993, DateTimeKind.Local).AddTicks(8432), "employee12@task.com", "Employee 12", false, "$2a$11$ugsC9vY0ybKfThBGUa.2SeDo41XOTWpmPWlby6SDJE/epxXKONubi", 3 },
                    { 20, new DateTime(2026, 5, 3, 16, 32, 49, 283, DateTimeKind.Local).AddTicks(2517), "employee13@task.com", "Employee 13", false, "$2a$11$LW5mJ/893NczwrqohQQq7uJii0Qj5bntNswP9FZAh/.hbtjKTBEF.", 3 },
                    { 21, new DateTime(2026, 5, 3, 16, 32, 49, 586, DateTimeKind.Local).AddTicks(6274), "employee14@task.com", "Employee 14", false, "$2a$11$dtcOQbFGyl1GhnTz2ntu1umocQ4GN.swDaQmjxFsHIsNV4SDOA0zi", 3 },
                    { 22, new DateTime(2026, 5, 3, 16, 32, 49, 889, DateTimeKind.Local).AddTicks(9573), "employee15@task.com", "Employee 15", false, "$2a$11$wN/gYqnBUIFU4bFw2fKWYOdgNJ.Wk2dm9noKpBZCqdlcrkQ/lbb/a", 3 },
                    { 23, new DateTime(2026, 5, 3, 16, 32, 50, 177, DateTimeKind.Local).AddTicks(4193), "employee16@task.com", "Employee 16", false, "$2a$11$t.T0SlxxElQb44ZrdrOfWuYOG5ftqBuHewwUqiuPah/SFdYxZTBgW", 3 },
                    { 24, new DateTime(2026, 5, 3, 16, 32, 50, 492, DateTimeKind.Local).AddTicks(5820), "employee17@task.com", "Employee 17", false, "$2a$11$XXntqtC4wW.CzHI8I1rkzO3Rjl2DXBZutYJhG4uHLbKFiME/Jhxzi", 3 },
                    { 25, new DateTime(2026, 5, 3, 16, 32, 50, 784, DateTimeKind.Local).AddTicks(4455), "employee18@task.com", "Employee 18", false, "$2a$11$pAwFDpu8OlGsm7cd6WNVA.2aJeCKBeTz37SCfGRvhPEiEpBaVJW96", 3 },
                    { 26, new DateTime(2026, 5, 3, 16, 32, 51, 66, DateTimeKind.Local).AddTicks(6924), "employee19@task.com", "Employee 19", false, "$2a$11$XFug77JieJuvyRSHemReuOyVxp892IGb22Q8dyhRY3.dqixrQTI56", 3 },
                    { 27, new DateTime(2026, 5, 3, 16, 32, 51, 379, DateTimeKind.Local).AddTicks(6901), "employee20@task.com", "Employee 20", false, "$2a$11$bhFUz3AXsjT0ibulnPF2VudBtgT.9PhXn0BBDpTgiAcw4cpzTey7S", 3 }
                });
        }
    }
}
