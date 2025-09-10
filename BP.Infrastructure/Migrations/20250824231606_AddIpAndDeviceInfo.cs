using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BP.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddIpAndDeviceInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastAccessIp",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastActiveDevice",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAccessIp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastActiveDevice",
                table: "Users");
        }
    }
}
