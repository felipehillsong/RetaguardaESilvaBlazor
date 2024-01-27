using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class ArrumandoCamposDePermissoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VisualizarVenda",
                table: "Permissao",
                newName: "VisualizarPedido");

            migrationBuilder.RenameColumn(
                name: "VendaExcluir",
                table: "Permissao",
                newName: "VisualizarNotaFiscal");

            migrationBuilder.RenameColumn(
                name: "VendaEditar",
                table: "Permissao",
                newName: "PedidoExcluir");

            migrationBuilder.RenameColumn(
                name: "VendaDetalhe",
                table: "Permissao",
                newName: "PedidoEditar");

            migrationBuilder.RenameColumn(
                name: "VendaCadastro",
                table: "Permissao",
                newName: "PedidoDetalhe");

            migrationBuilder.AddColumn<bool>(
                name: "NotaFiscalCadastro",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotaFiscalDetalhe",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotaFiscalEditar",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotaFiscalExcluir",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PedidoCadastro",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotaFiscalCadastro",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "NotaFiscalDetalhe",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "NotaFiscalEditar",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "NotaFiscalExcluir",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "PedidoCadastro",
                table: "Permissao");

            migrationBuilder.RenameColumn(
                name: "VisualizarPedido",
                table: "Permissao",
                newName: "VisualizarVenda");

            migrationBuilder.RenameColumn(
                name: "VisualizarNotaFiscal",
                table: "Permissao",
                newName: "VendaExcluir");

            migrationBuilder.RenameColumn(
                name: "PedidoExcluir",
                table: "Permissao",
                newName: "VendaEditar");

            migrationBuilder.RenameColumn(
                name: "PedidoEditar",
                table: "Permissao",
                newName: "VendaDetalhe");

            migrationBuilder.RenameColumn(
                name: "PedidoDetalhe",
                table: "Permissao",
                newName: "VendaCadastro");
        }
    }
}
