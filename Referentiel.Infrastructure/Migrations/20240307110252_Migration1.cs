using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Referentiel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeuresTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Admin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectQuotation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quotation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectQuotation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectQuotation_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Progress = table.Column<float>(type: "real", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectStatistic_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaginationAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PagAdminUsers = table.Column<int>(type: "int", nullable: false),
                    PagAdminPublications = table.Column<int>(type: "int", nullable: false),
                    PagAdminProjects = table.Column<int>(type: "int", nullable: false),
                    PagAdminQuotations = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaginationAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaginationAdmin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaginationUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PagUserWeeks = table.Column<int>(type: "int", nullable: false),
                    PagUserPublications = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaginationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaginationUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Heure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publication_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserWeek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWeek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWeek_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserWeekProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quotation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoAppMonday = table.Column<int>(type: "int", nullable: false),
                    NoAppTuesday = table.Column<int>(type: "int", nullable: false),
                    NoAppWednesday = table.Column<int>(type: "int", nullable: false),
                    NoAppThursday = table.Column<int>(type: "int", nullable: false),
                    NoAppFriday = table.Column<int>(type: "int", nullable: false),
                    NoAppSaturday = table.Column<int>(type: "int", nullable: false),
                    NoAppSunday = table.Column<int>(type: "int", nullable: false),
                    MondayStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TuesdayStaus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WednesdayStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThursdayStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FridayStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaturdayStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SundayStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppMonday = table.Column<int>(type: "int", nullable: false),
                    AppTuesday = table.Column<int>(type: "int", nullable: false),
                    AppWednesday = table.Column<int>(type: "int", nullable: false),
                    AppThursday = table.Column<int>(type: "int", nullable: false),
                    AppFriday = table.Column<int>(type: "int", nullable: false),
                    AppSaturday = table.Column<int>(type: "int", nullable: false),
                    AppSunday = table.Column<int>(type: "int", nullable: false),
                    Bonus = table.Column<int>(type: "int", nullable: false),
                    TotalApp = table.Column<int>(type: "int", nullable: false),
                    TotalNoApp = table.Column<int>(type: "int", nullable: false),
                    WeekId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWeekProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWeekProject_UserWeek_WeekId",
                        column: x => x.WeekId,
                        principalTable: "UserWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWeekProject_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaginationAdmin_UserId",
                table: "PaginationAdmin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaginationUser_UserId",
                table: "PaginationUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectQuotation_ProjectId",
                table: "ProjectQuotation",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStatistic_ProjectId",
                table: "ProjectStatistic",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_UserId",
                table: "Publication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWeek_UserId",
                table: "UserWeek",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWeekProject_UserId",
                table: "UserWeekProject",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWeekProject_WeekId",
                table: "UserWeekProject",
                column: "WeekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaginationAdmin");

            migrationBuilder.DropTable(
                name: "PaginationUser");

            migrationBuilder.DropTable(
                name: "ProjectQuotation");

            migrationBuilder.DropTable(
                name: "ProjectStatistic");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropTable(
                name: "UserWeekProject");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "UserWeek");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
