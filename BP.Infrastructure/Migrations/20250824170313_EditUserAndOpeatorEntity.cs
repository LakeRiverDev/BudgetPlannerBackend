using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BP.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class EditUserAndOpeatorEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Operators",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Operators",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Operators");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Operators");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
