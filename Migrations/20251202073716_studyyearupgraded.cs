using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saturday_Back.Migrations
{
    /// <inheritdoc />
    public partial class studyyearupgraded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_study_years_yearrange",
                table: "study_years");

            migrationBuilder.DropColumn(
                name: "yearrange",
                table: "study_years");

            migrationBuilder.AddColumn<string>(
                name: "year_range",
                table: "study_years",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "year_range",
                table: "study_years");

            migrationBuilder.AddColumn<string>(
                name: "yearrange",
                table: "study_years",
                type: "varchar(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_study_years_yearrange",
                table: "study_years",
                column: "yearrange",
                unique: true);
        }
    }
}
