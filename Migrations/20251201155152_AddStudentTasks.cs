using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Planner_API.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Tasks_TaskEntityId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_TaskEntityId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TaskEntityId",
                table: "Students");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentEntityTaskEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskEntityId",
                table: "Students",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_TaskEntityId",
                table: "Students",
                column: "TaskEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Tasks_TaskEntityId",
                table: "Students",
                column: "TaskEntityId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
