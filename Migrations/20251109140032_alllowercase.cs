using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saturday_Back.Migrations
{
    /// <inheritdoc />
    public partial class alllowercase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "payment_types",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "payment_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "payment_types",
                newName: "discount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "value",
                table: "payment_types",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "payment_types",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "discount",
                table: "payment_types",
                newName: "Discount");
        }
    }
}
