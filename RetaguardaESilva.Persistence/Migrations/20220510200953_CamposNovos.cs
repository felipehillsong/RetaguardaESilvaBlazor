using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class CamposNovos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InscricaoEstadual",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InscricaoMunicipal",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InscricaoEstadual",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InscricaoMunicipal",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InscricaoEstadual",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "InscricaoMunicipal",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "InscricaoEstadual",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "InscricaoMunicipal",
                table: "Cliente");
        }
    }
}
