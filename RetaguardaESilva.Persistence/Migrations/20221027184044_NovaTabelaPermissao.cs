using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetaguardaESilva.Persistence.Migrations
{
    public partial class NovaTabelaPermissao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteCadastro = table.Column<bool>(type: "bit", nullable: false),
                    ClienteEditar = table.Column<bool>(type: "bit", nullable: false),
                    ClienteDetalhe = table.Column<bool>(type: "bit", nullable: false),
                    ClienteExcluir = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaCadastro = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaEditar = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaDetalhe = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaExcluir = table.Column<bool>(type: "bit", nullable: false),
                    EstoqueCadastro = table.Column<bool>(type: "bit", nullable: false),
                    EstoqueEditar = table.Column<bool>(type: "bit", nullable: false),
                    EstoqueDetalhe = table.Column<bool>(type: "bit", nullable: false),
                    EstoqueExcluir = table.Column<bool>(type: "bit", nullable: false),
                    FornecedorCadastro = table.Column<bool>(type: "bit", nullable: false),
                    FornecedorEditar = table.Column<bool>(type: "bit", nullable: false),
                    FornecedorDetalhe = table.Column<bool>(type: "bit", nullable: false),
                    FornecedorExcluir = table.Column<bool>(type: "bit", nullable: false),
                    FuncionarioCadastro = table.Column<bool>(type: "bit", nullable: false),
                    FuncionarioEditar = table.Column<bool>(type: "bit", nullable: false),
                    FuncionarioDetalhe = table.Column<bool>(type: "bit", nullable: false),
                    FuncionarioExcluir = table.Column<bool>(type: "bit", nullable: false),
                    ProdutoCadastro = table.Column<bool>(type: "bit", nullable: false),
                    ProdutoEditar = table.Column<bool>(type: "bit", nullable: false),
                    ProdutoDetalhe = table.Column<bool>(type: "bit", nullable: false),
                    ProdutoExcluir = table.Column<bool>(type: "bit", nullable: false),
                    RelatorioCadastro = table.Column<bool>(type: "bit", nullable: false),
                    RelatorioEditar = table.Column<bool>(type: "bit", nullable: false),
                    RelatorioDetalhe = table.Column<bool>(type: "bit", nullable: false),
                    RelatorioExcluir = table.Column<bool>(type: "bit", nullable: false),
                    TransportadorCadastro = table.Column<bool>(type: "bit", nullable: false),
                    TransportadorEditar = table.Column<bool>(type: "bit", nullable: false),
                    TransportadorDetalhe = table.Column<bool>(type: "bit", nullable: false),
                    TransportadorExcluir = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCadastro = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioEditar = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioDetalhe = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioExcluir = table.Column<bool>(type: "bit", nullable: false),
                    VendaCadastro = table.Column<bool>(type: "bit", nullable: false),
                    VendaEditar = table.Column<bool>(type: "bit", nullable: false),
                    VendaDetalhe = table.Column<bool>(type: "bit", nullable: false),
                    VendaExcluir = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissao", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
