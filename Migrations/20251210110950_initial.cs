using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saturday_Back.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "academic_years",
                columns: table => new
                {
                    rec_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    year_range = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cost = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_academic_years", x => x.rec_id);
                    table.CheckConstraint("CK_AcademicYear_YearRange_Format", "`year_range` REGEXP '^[0-9]{4}-[0-9]{4}$'");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "benefit_types",
                columns: table => new
                {
                    rec_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    discount = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_benefit_types", x => x.rec_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "payment_types",
                columns: table => new
                {
                    rec_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    discount = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_types", x => x.rec_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    rec_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.rec_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    rec_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    identificator = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    firstname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    admissionyearid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.rec_id);
                    table.ForeignKey(
                        name: "FK_students_academic_years_admissionyearid",
                        column: x => x.admissionyearid,
                        principalTable: "academic_years",
                        principalColumn: "rec_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    rec_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    studentid = table.Column<int>(type: "int", nullable: false),
                    subjectid = table.Column<int>(type: "int", nullable: false),
                    paymenttypeid = table.Column<int>(type: "int", nullable: false),
                    benefittypeid = table.Column<int>(type: "int", nullable: false),
                    firstsaturday = table.Column<int>(type: "int", nullable: false),
                    lastsaturday = table.Column<int>(type: "int", nullable: false),
                    firstmonth = table.Column<int>(type: "int", nullable: false),
                    lastmonth = table.Column<int>(type: "int", nullable: false),
                    studyyear = table.Column<string>(type: "char(9)", maxLength: 9, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    schedule_entries = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedules", x => x.rec_id);
                    table.ForeignKey(
                        name: "FK_schedules_benefit_types_benefittypeid",
                        column: x => x.benefittypeid,
                        principalTable: "benefit_types",
                        principalColumn: "rec_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedules_payment_types_paymenttypeid",
                        column: x => x.paymenttypeid,
                        principalTable: "payment_types",
                        principalColumn: "rec_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedules_students_studentid",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "rec_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedules_subjects_subjectid",
                        column: x => x.subjectid,
                        principalTable: "subjects",
                        principalColumn: "rec_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_academic_years_year_range",
                table: "academic_years",
                column: "year_range",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_schedules_benefittypeid",
                table: "schedules",
                column: "benefittypeid");

            migrationBuilder.CreateIndex(
                name: "IX_schedules_paymenttypeid",
                table: "schedules",
                column: "paymenttypeid");

            migrationBuilder.CreateIndex(
                name: "IX_schedules_studentid",
                table: "schedules",
                column: "studentid");

            migrationBuilder.CreateIndex(
                name: "IX_schedules_subjectid",
                table: "schedules",
                column: "subjectid");

            migrationBuilder.CreateIndex(
                name: "IX_students_admissionyearid",
                table: "students",
                column: "admissionyearid");

            migrationBuilder.CreateIndex(
                name: "IX_students_identificator",
                table: "students",
                column: "identificator",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subjects_name",
                table: "subjects",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schedules");

            migrationBuilder.DropTable(
                name: "benefit_types");

            migrationBuilder.DropTable(
                name: "payment_types");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "academic_years");
        }
    }
}
