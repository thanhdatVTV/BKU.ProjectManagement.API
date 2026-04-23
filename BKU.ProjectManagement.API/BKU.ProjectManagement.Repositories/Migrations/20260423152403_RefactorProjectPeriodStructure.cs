using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BKU.ProjectManagement.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class RefactorProjectPeriodStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentLockAt",
                table: "ProjectPeriod");

            migrationBuilder.DropColumn(
                name: "FinalSubmitEnd",
                table: "ProjectPeriod");

            migrationBuilder.DropColumn(
                name: "FinalSubmitStart",
                table: "ProjectPeriod");

            migrationBuilder.DropColumn(
                name: "ProgressEnd",
                table: "ProjectPeriod");

            migrationBuilder.DropColumn(
                name: "ProgressStart",
                table: "ProjectPeriod");

            migrationBuilder.DropColumn(
                name: "RegistrationEnd",
                table: "ProjectPeriod");

            migrationBuilder.DropColumn(
                name: "RegistrationStart",
                table: "ProjectPeriod");

            migrationBuilder.RenameColumn(
                name: "ReviewStart",
                table: "ProjectPeriod",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "ReviewEnd",
                table: "ProjectPeriod",
                newName: "EndDate");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ProjectPeriod",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProjectPeriod");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "ProjectPeriod",
                newName: "ReviewStart");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "ProjectPeriod",
                newName: "ReviewEnd");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignmentLockAt",
                table: "ProjectPeriod",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalSubmitEnd",
                table: "ProjectPeriod",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalSubmitStart",
                table: "ProjectPeriod",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProgressEnd",
                table: "ProjectPeriod",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProgressStart",
                table: "ProjectPeriod",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationEnd",
                table: "ProjectPeriod",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationStart",
                table: "ProjectPeriod",
                type: "datetime2",
                nullable: true);
        }
    }
}
