using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class CampoNovoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstoqueCadastro",
                table: "Permissao");

            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "Produto",
                newName: "PrecoCompra");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoVenda",
                table: "Produto",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoCompra",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "PrecoCompra",
                table: "Produto",
                newName: "Preco");

            migrationBuilder.AddColumn<bool>(
                name: "EstoqueCadastro",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
