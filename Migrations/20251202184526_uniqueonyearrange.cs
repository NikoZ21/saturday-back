using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saturday_Back.Migrations
{
    /// <inheritdoc />
    public partial class uniqueonyearrange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_study_years_year_range",
                table: "study_years",
                column: "year_range",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_study_years_year_range",
                table: "study_years");
        }
    }
}
