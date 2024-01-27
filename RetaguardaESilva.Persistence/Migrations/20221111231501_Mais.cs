using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class Mais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioDetalhe",
                table: "Permissao",
                newName: "VisualizarVenda");

            migrationBuilder.AddColumn<bool>(
                name: "UsuarioPermissoes",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VisualizarCliente",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VisualizarEmpresa",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VisualizarEstoque",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VisualizarFornecedor",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VisualizarFuncionario",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VisualizarProduto",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VisualizarRelatorio",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VisualizarTransportador",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VisualizarUsuario",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioPermissoes",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "VisualizarCliente",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "VisualizarEmpresa",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "VisualizarEstoque",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "VisualizarFornecedor",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "VisualizarFuncionario",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "VisualizarProduto",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "VisualizarRelatorio",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "VisualizarTransportador",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "VisualizarUsuario",
                table: "Permissao");

            migrationBuilder.RenameColumn(
                name: "VisualizarVenda",
                table: "Permissao",
                newName: "UsuarioDetalhe");
        }
    }
}
