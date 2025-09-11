using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BP.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddLimitFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LimitPerDay",
                table: "Accounts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LimitPerMonth",
                table: "Accounts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitPerDay",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LimitPerMonth",
                table: "Accounts");
        }
    }
}
