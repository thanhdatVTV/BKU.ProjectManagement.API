using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BKU.ProjectManagement.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamManagementFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InvitedBy",
                table: "ProjectTeamMember",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProjectTeamMember",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxTeamMembers",
                table: "ProjectPeriod",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitedBy",
                table: "ProjectTeamMember");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProjectTeamMember");

            migrationBuilder.DropColumn(
                name: "MaxTeamMembers",
                table: "ProjectPeriod");
        }
    }
}
