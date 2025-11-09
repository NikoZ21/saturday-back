using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saturday_Back.Migrations
{
    /// <inheritdoc />
    public partial class Payments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "payment_types",
                newName: "rec_id");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "payment_types",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "payment_types",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "payment_types");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "payment_types");

            migrationBuilder.RenameColumn(
                name: "rec_id",
                table: "payment_types",
                newName: "Id");
        }
    }
}
