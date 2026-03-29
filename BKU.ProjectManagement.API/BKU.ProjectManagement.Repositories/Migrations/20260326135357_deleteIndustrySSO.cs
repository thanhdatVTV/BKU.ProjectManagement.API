using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BKU.ProjectManagement.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class deleteIndustrySSO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblIndustry",
                schema: "SSO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblIndustry",
                schema: "SSO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "system"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    IndustryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_TblIndustry_FacultyId",
                schema: "SSO",
                table: "TblIndustry",
                column: "FacultyId");
        }
    }
}
