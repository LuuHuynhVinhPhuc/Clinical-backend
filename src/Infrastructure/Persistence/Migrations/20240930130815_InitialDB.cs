using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClinicalBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowUps_Patients_PatientId",
                table: "FollowUps");

            migrationBuilder.DropColumn(
                name: "Medicines",
                table: "Prescriptions");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Prescriptions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Prescriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "Prescriptions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Prescriptions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Prescriptions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrescriptionDrug",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicineID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Morning = table.Column<int>(type: "integer", nullable: false),
                    Noon = table.Column<int>(type: "integer", nullable: false),
                    Night = table.Column<int>(type: "integer", nullable: false),
                    Usage = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    PrescriptionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionDrug", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionDrug_Medicines_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionDrug_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDrug_MedicineID",
                table: "PrescriptionDrug",
                column: "MedicineID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDrug_PrescriptionId",
                table: "PrescriptionDrug",
                column: "PrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUps_Patients_PatientId",
                table: "FollowUps",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowUps_Patients_PatientId",
                table: "FollowUps");

            migrationBuilder.DropTable(
                name: "PrescriptionDrug");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Prescriptions");

            migrationBuilder.AddColumn<Guid[]>(
                name: "Medicines",
                table: "Prescriptions",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUps_Patients_PatientId",
                table: "FollowUps",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
