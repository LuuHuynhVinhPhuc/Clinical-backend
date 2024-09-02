using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FollowUpMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientsInfo",
                table: "PatientsInfo");

            migrationBuilder.RenameTable(
                name: "PatientsInfo",
                newName: "Patients");

            migrationBuilder.AddColumn<DateTime>(
                name: "dateCreated",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateModified",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateCreated",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateModified",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateCreated",
                table: "Medicines",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateModified",
                table: "Medicines",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateCreated",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateModified",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FollowUps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckUp = table.Column<string>(type: "text", nullable: true),
                    History = table.Column<string>(type: "text", nullable: true),
                    Diagnosis = table.Column<string>(type: "text", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUps", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowUps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "dateCreated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "dateModified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "dateCreated",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "dateModified",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "dateCreated",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "dateModified",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "dateCreated",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "dateModified",
                table: "Patients");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "PatientsInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientsInfo",
                table: "PatientsInfo",
                column: "Id");
        }
    }
}
