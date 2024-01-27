using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class RemovendoCamposDesnecessarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "NotaFiscal");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Pedido",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FornecedorId",
                table: "Pedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FornecedorId",
                table: "NotaFiscal",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
