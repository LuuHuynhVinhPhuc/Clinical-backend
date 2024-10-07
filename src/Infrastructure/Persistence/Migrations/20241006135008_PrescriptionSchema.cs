using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PrescriptionSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionDrug_Prescriptions_PrescriptionId",
                table: "PrescriptionDrug");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Medicines_MedicineId",
                table: "Prescriptions");

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

            migrationBuilder.DropColumn(
                name: "Morning",
                table: "PrescriptionDrug");

            migrationBuilder.DropColumn(
                name: "Night",
                table: "PrescriptionDrug");

            migrationBuilder.DropColumn(
                name: "Noon",
                table: "PrescriptionDrug");

            migrationBuilder.RenameColumn(
                name: "MedicineId",
                table: "Prescriptions",
                newName: "FollowUpID");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_MedicineId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_FollowUpID");

            migrationBuilder.RenameColumn(
                name: "PrescriptionId",
                table: "PrescriptionDrug",
                newName: "PrescriptionID");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptionDrug_PrescriptionId",
                table: "PrescriptionDrug",
                newName: "IX_PrescriptionDrug_PrescriptionID");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "PrescriptionDrug",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionDrug_Prescriptions_PrescriptionID",
                table: "PrescriptionDrug",
                column: "PrescriptionID",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_FollowUps_FollowUpID",
                table: "Prescriptions",
                column: "FollowUpID",
                principalTable: "FollowUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionDrug_Prescriptions_PrescriptionID",
                table: "PrescriptionDrug");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_FollowUps_FollowUpID",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "PrescriptionDrug");

            migrationBuilder.RenameColumn(
                name: "FollowUpID",
                table: "Prescriptions",
                newName: "MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_FollowUpID",
                table: "Prescriptions",
                newName: "IX_Prescriptions_MedicineId");

            migrationBuilder.RenameColumn(
                name: "PrescriptionID",
                table: "PrescriptionDrug",
                newName: "PrescriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptionDrug_PrescriptionID",
                table: "PrescriptionDrug",
                newName: "IX_PrescriptionDrug_PrescriptionId");

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

            migrationBuilder.AddColumn<int>(
                name: "Morning",
                table: "PrescriptionDrug",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Night",
                table: "PrescriptionDrug",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Noon",
                table: "PrescriptionDrug",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionDrug_Prescriptions_PrescriptionId",
                table: "PrescriptionDrug",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Medicines_MedicineId",
                table: "Prescriptions",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}