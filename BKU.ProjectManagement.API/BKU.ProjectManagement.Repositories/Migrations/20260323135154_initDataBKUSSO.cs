using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BKU.ProjectManagement.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class initDataBKUSSO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SSO");

            migrationBuilder.CreateTable(
                name: "TblClassGroup",
                schema: "SSO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassGroupName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblClassGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblCourse",
                schema: "SSO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblUser",
                schema: "SSO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblFaculty",
                schema: "SSO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    FacultyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblFaculty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblFaculty_TblCourse_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "SSO",
                        principalTable: "TblCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblTeacher",
                schema: "SSO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblTeacher_TblCourse_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "SSO",
                        principalTable: "TblCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblTeacher_TblUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "SSO",
                        principalTable: "TblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblIndustry",
                schema: "SSO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    IndustryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblIndustry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblIndustry_TblFaculty_FacultyId",
                        column: x => x.FacultyId,
                        principalSchema: "SSO",
                        principalTable: "TblFaculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblMajor",
                schema: "SSO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    MajorName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblMajor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblMajor_TblFaculty_FacultyId",
                        column: x => x.FacultyId,
                        principalSchema: "SSO",
                        principalTable: "TblFaculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblStudent",
                schema: "SSO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MajorId = table.Column<int>(type: "int", nullable: false),
                    ClassGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblStudent_TblClassGroup_ClassGroupId",
                        column: x => x.ClassGroupId,
                        principalSchema: "SSO",
                        principalTable: "TblClassGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblStudent_TblMajor_MajorId",
                        column: x => x.MajorId,
                        principalSchema: "SSO",
                        principalTable: "TblMajor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblStudent_TblUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "SSO",
                        principalTable: "TblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblFaculty_CourseId",
                schema: "SSO",
                table: "TblFaculty",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TblIndustry_FacultyId",
                schema: "SSO",
                table: "TblIndustry",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_TblMajor_FacultyId",
                schema: "SSO",
                table: "TblMajor",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_TblStudent_ClassGroupId",
                schema: "SSO",
                table: "TblStudent",
                column: "ClassGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TblStudent_MajorId",
                schema: "SSO",
                table: "TblStudent",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_TblStudent_StudentId",
                schema: "SSO",
                table: "TblStudent",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblStudent_UserId",
                schema: "SSO",
                table: "TblStudent",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblTeacher_CourseId",
                schema: "SSO",
                table: "TblTeacher",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TblTeacher_TeacherId",
                schema: "SSO",
                table: "TblTeacher",
                column: "TeacherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblTeacher_UserId",
                schema: "SSO",
                table: "TblTeacher",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblUser_UserName",
                schema: "SSO",
                table: "TblUser",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblIndustry",
                schema: "SSO");

            migrationBuilder.DropTable(
                name: "TblStudent",
                schema: "SSO");

            migrationBuilder.DropTable(
                name: "TblTeacher",
                schema: "SSO");

            migrationBuilder.DropTable(
                name: "TblClassGroup",
                schema: "SSO");

            migrationBuilder.DropTable(
                name: "TblMajor",
                schema: "SSO");

            migrationBuilder.DropTable(
                name: "TblUser",
                schema: "SSO");

            migrationBuilder.DropTable(
                name: "TblFaculty",
                schema: "SSO");

            migrationBuilder.DropTable(
                name: "TblCourse",
                schema: "SSO");
        }
    }
}
