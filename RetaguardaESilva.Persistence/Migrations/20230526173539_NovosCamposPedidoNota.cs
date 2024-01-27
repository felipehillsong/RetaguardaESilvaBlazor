using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class NovosCamposPedidoNota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CodigoProduto",
                table: "PedidoNota",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "NomeProduto",
                table: "PedidoNota",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoProduto",
                table: "PedidoNota");

            migrationBuilder.DropColumn(
                name: "NomeProduto",
                table: "PedidoNota");
        }
    }
}
