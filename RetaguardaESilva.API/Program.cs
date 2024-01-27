using Microsoft.EntityFrameworkCore;
using RetaguardaESilva;
using Microsoft.Extensions.DependencyInjection;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.PersistenciaService;
using RetaguardaESilva.Persistence.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using RetaguardaESilva.Persistence.Contratos;
using RetaguardaESilva.Persistence.Persistencias;
using ProEventos.Persistence.Persistencias;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClientePersist, ClientePersist>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IEmpresaPersist, EmpresaPersist>();
builder.Services.AddScoped<IEstoqueService, EstoqueService>();
builder.Services.AddScoped<IEstoquePersist, EstoquePersist>();
builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<IFornecedorPersist, FornecedorPersist>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
builder.Services.AddScoped<IFuncionarioPersist, FuncionarioPersist>();
builder.Services.AddScoped<IGeralPersist, GeralPersist>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<INotaFiscalService, NotaFiscalService>();
builder.Services.AddScoped<INotaFiscalPersist, NotaFiscalPersist>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPedidoPersist, PedidoPersist>();
builder.Services.AddScoped<IPedidoNotaPersist, PedidoNotaPersist>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoPersist, ProdutoPersist>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();
builder.Services.AddScoped<IRelatorioPersist, RelatorioPersist>();
builder.Services.AddScoped<ITransportadorService, TransportadorService>();
builder.Services.AddScoped<ITransportadorPersist, TransportadorPersist>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioPersist, UsuarioPersist>();
builder.Services.AddScoped<IValidacoesPersist, ValidacoesPersist>();
builder.Services.AddDbContext<RetaguardaESilvaContext>(context =>
{
    context.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    context.EnableSensitiveDataLogging();
});
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});


var app = builder.Build();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();