using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class PermissaoEnderecoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EnderecoProdutoDetalhe",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnderecoProdutoEditar",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnderecoProdutoExcluir",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VisualizarEnderecoProduto",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnderecoProdutoDetalhe",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "EnderecoProdutoEditar",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "EnderecoProdutoExcluir",
                table: "Permissao");

            migrationBuilder.DropColumn(
                name: "VisualizarEnderecoProduto",
                table: "Permissao");
        }
    }
}
