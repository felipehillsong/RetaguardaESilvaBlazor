using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class EclusaoCampoStatusProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "Status",
               table: "Produto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
