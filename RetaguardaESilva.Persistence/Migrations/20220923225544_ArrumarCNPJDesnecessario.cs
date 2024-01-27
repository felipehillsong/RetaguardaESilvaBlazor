using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class ArrumarCNPJDesnecessario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPFCNPJ",
                table: "Transportador",
                newName: "CNPJ");

            migrationBuilder.RenameColumn(
                name: "CPFCNPJ",
                table: "Fornecedor",
                newName: "CNPJ");

            migrationBuilder.RenameColumn(
                name: "CPFCNPJ",
                table: "Empresa",
                newName: "CNPJ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Transportador",
                newName: "CPFCNPJ");

            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Fornecedor",
                newName: "CPFCNPJ");

            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Empresa",
                newName: "CPFCNPJ");
        }
    }
}
