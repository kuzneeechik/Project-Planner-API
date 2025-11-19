using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Planner_API.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Results",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_ParentId",
                table: "Results",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Tasks_ParentId",
                table: "Results",
                column: "ParentId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Tasks_ParentId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_ParentId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Results");
        }
    }
}
