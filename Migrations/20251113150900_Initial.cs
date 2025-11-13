using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Planner_API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentEntityTeamEntity",
                columns: table => new
                {
                    StudentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEntityTeamEntity", x => new { x.StudentsId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_StudentEntityTeamEntity_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEntityTeamEntity_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subjects_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ResponsibleTeamId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    ResultEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Results_ResultEntityId",
                        column: x => x.ResultEntityId,
                        principalTable: "Results",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Tasks_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Teams_ResponsibleTeamId",
                        column: x => x.ResponsibleTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentEntityTaskEntity",
                columns: table => new
                {
                    ResponsibleStudentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TasksId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEntityTaskEntity", x => new { x.ResponsibleStudentsId, x.TasksId });
                    table.ForeignKey(
                        name: "FK_StudentEntityTaskEntity_Students_ResponsibleStudentsId",
                        column: x => x.ResponsibleStudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEntityTaskEntity_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentEntityTaskEntity_TasksId",
                table: "StudentEntityTaskEntity",
                column: "TasksId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEntityTeamEntity_TeamsId",
                table: "StudentEntityTeamEntity",
                column: "TeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ResultId",
                table: "Subjects",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TeamId",
                table: "Subjects",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ParentId",
                table: "Tasks",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ResponsibleTeamId",
                table: "Tasks",
                column: "ResponsibleTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ResultEntityId",
                table: "Tasks",
                column: "ResultEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentEntityTaskEntity");

            migrationBuilder.DropTable(
                name: "StudentEntityTeamEntity");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
