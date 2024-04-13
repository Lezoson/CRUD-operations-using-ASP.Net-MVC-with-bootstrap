using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsMangement.Migrations
{
    /// <inheritdoc />
    public partial class InitialBuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_CourseDetails_CourseDetailsID",
                table: "StudentCourse");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourse_CourseDetailsID",
                table: "StudentCourse");

            migrationBuilder.DropColumn(
                name: "CourseDetailsID",
                table: "StudentCourse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseDetailsID",
                table: "StudentCourse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseDetailsID",
                table: "StudentCourse",
                column: "CourseDetailsID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_CourseDetails_CourseDetailsID",
                table: "StudentCourse",
                column: "CourseDetailsID",
                principalTable: "CourseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
