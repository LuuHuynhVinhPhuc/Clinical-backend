using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialEditPatientProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientName",
                table: "PatientsInfo",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PatientsInfo",
                newName: "PatientName");
        }
    }
}
