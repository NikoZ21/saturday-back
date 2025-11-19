using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saturday_Back.Migrations
{
    /// <inheritdoc />
    public partial class updatedBaseCostsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "subjects",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_subjects_Name",
                table: "subjects",
                newName: "IX_subjects_name");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "base_costs",
                newName: "cost");

            migrationBuilder.RenameColumn(
                name: "StudyYear",
                table: "base_costs",
                newName: "study_year");

            migrationBuilder.RenameIndex(
                name: "IX_base_costs_StudyYear",
                table: "base_costs",
                newName: "IX_base_costs_study_year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "subjects",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_subjects_name",
                table: "subjects",
                newName: "IX_subjects_Name");

            migrationBuilder.RenameColumn(
                name: "cost",
                table: "base_costs",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "study_year",
                table: "base_costs",
                newName: "StudyYear");

            migrationBuilder.RenameIndex(
                name: "IX_base_costs_study_year",
                table: "base_costs",
                newName: "IX_base_costs_StudyYear");
        }
    }
}
