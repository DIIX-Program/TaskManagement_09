using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMembers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMembers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    AssignedTo = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<byte>(type: "tinyint", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_AssignedTo",
                        column: x => x.AssignedTo,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_ProjectId_UserId",
                table: "ProjectMembers",
                columns: new[] { "ProjectId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_UserId",
                table: "ProjectMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedBy",
                table: "Projects",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedTo",
                table: "Tasks",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusId",
                table: "Tasks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ProjectMembers");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TaskStatuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
