using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class ArrumandoPermissaoNF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotaFiscalDetalhe",
                table: "Permissao");

            migrationBuilder.RenameColumn(
                name: "NotaFiscalExcluir",
                table: "Permissao",
                newName: "NotaFiscalGerarPDF");

            migrationBuilder.RenameColumn(
                name: "NotaFiscalEditar",
                table: "Permissao",
                newName: "NotaFiscalCancelar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotaFiscalGerarPDF",
                table: "Permissao",
                newName: "NotaFiscalExcluir");

            migrationBuilder.RenameColumn(
                name: "NotaFiscalCancelar",
                table: "Permissao",
                newName: "NotaFiscalEditar");

            migrationBuilder.AddColumn<bool>(
                name: "NotaFiscalDetalhe",
                table: "Permissao",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
