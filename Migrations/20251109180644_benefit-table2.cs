using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saturday_Back.Migrations
{
    /// <inheritdoc />
    public partial class benefittable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "benefit_types",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "benefit_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "benefit_types",
                newName: "discount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "value",
                table: "benefit_types",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "benefit_types",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "discount",
                table: "benefit_types",
                newName: "Discount");
        }
    }
}
