using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BP.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDateOperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Operators_OperatorId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "DateOperation",
                table: "Operations");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentType",
                table: "Operations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentCategory",
                table: "Operations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OperatorId",
                table: "Operations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Operators_OperatorId",
                table: "Operations",
                column: "OperatorId",
                principalTable: "Operators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Operators_OperatorId",
                table: "Operations");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentType",
                table: "Operations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentCategory",
                table: "Operations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "OperatorId",
                table: "Operations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOperation",
                table: "Operations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Operators_OperatorId",
                table: "Operations",
                column: "OperatorId",
                principalTable: "Operators",
                principalColumn: "Id");
        }
    }
}
