using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialDOB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DOB",
                table: "PatientsInfo",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "PatientsInfo");
        }
    }
}
