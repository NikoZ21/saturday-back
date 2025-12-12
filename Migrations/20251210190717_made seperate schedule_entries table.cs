using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saturday_Back.Migrations
{
    /// <inheritdoc />
    public partial class madeseperateschedule_entriestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "schedule_entries",
                table: "schedules");

            migrationBuilder.CreateTable(
                name: "schedule_entries",
                columns: table => new
                {
                    rec_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<string>(type: "VARCHAR(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    schedule_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule_entries", x => x.rec_id);
                    table.ForeignKey(
                        name: "FK_schedule_entries_schedules_schedule_id",
                        column: x => x.schedule_id,
                        principalTable: "schedules",
                        principalColumn: "rec_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_entries_schedule_id",
                table: "schedule_entries",
                column: "schedule_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schedule_entries");

            migrationBuilder.AddColumn<string>(
                name: "schedule_entries",
                table: "schedules",
                type: "json",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
