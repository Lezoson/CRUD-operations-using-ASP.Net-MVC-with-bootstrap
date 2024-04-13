using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsMangement.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseDuration = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentDetails",
                columns: table => new
                {
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDetails", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_StudentDetails_CourseDetails_CourseDetailsId",
                        column: x => x.CourseDetailsId,
                        principalTable: "CourseDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentAddress",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressLineOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLineTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressStudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAddress", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_StudentAddress_StudentDetails_AddressStudentID",
                        column: x => x.AddressStudentID,
                        principalTable: "StudentDetails",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseDetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseStudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_StudentCourse_CourseDetails_CourseDetailsID",
                        column: x => x.CourseDetailsID,
                        principalTable: "CourseDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_StudentDetails_CourseStudentID",
                        column: x => x.CourseStudentID,
                        principalTable: "StudentDetails",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAddress_AddressStudentID",
                table: "StudentAddress",
                column: "AddressStudentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseDetailsID",
                table: "StudentCourse",
                column: "CourseDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseStudentID",
                table: "StudentCourse",
                column: "CourseStudentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentDetails_CourseDetailsId",
                table: "StudentDetails",
                column: "CourseDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAddress");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "StudentDetails");

            migrationBuilder.DropTable(
                name: "CourseDetails");
        }
    }
}
