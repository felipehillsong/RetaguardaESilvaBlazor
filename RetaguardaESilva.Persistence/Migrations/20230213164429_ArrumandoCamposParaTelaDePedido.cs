using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class ArrumandoCamposParaTelaDePedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "PedidoNota");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "NotaFiscal");         

            migrationBuilder.RenameColumn(
                name: "PrecoVenda",
                table: "Pedido",
                newName: "PrecoTotal");

            migrationBuilder.AddColumn<int>(
                name: "FornecedorId",
                table: "PedidoNota",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PedidoNota",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FornecedorId",
                table: "NotaFiscal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoTotal",
                table: "NotaFiscal",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeItens",
                table: "NotaFiscal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "NotaFiscal",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "PedidoNota");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PedidoNota");

            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "NotaFiscal");

            migrationBuilder.DropColumn(
                name: "PrecoTotal",
                table: "NotaFiscal");

            migrationBuilder.DropColumn(
                name: "QuantidadeItens",
                table: "NotaFiscal");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NotaFiscal");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Pedido",
                newName: "Quantidade");

            migrationBuilder.RenameColumn(
                name: "PrecoTotal",
                table: "Pedido",
                newName: "PrecoVenda");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "PedidoNota",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Pedido",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "NotaFiscal",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
