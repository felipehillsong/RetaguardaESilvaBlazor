using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class ArrumandoCampoNF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PedidoNotaId",
                table: "NotaFiscal",
                newName: "PedidoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "NotaFiscal",
                newName: "PedidoNotaId");
        }
    }
}
