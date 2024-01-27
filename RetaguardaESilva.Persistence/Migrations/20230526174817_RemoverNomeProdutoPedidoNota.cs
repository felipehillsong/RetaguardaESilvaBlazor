using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class RemoverNomeProdutoPedidoNota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeProduto",
                table: "PedidoNota");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeProduto",
                table: "PedidoNota",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
