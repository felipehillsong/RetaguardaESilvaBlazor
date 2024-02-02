using Microsoft.EntityFrameworkCore;
using RetaguardaESilva.Application.ContratosServices;
using RetaguardaESilva.Application.PersistenciaService;
using RetaguardaESilva.Persistence.Data;
using RetaguardaESilva.Persistence.Contratos;
using RetaguardaESilva.Persistence.Persistencias;
using ProEventos.Persistence.Persistencias;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddCors();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
    c.AddPolicy("AnotherPolicy", options => options.WithOrigins("https://localhost:7104"));
});

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<RetaguardaESilvaContext>()
    .AddDefaultTokenProviders();

var configuration = builder.Configuration;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(configuration["jwt:key"])),
        ClockSkew = TimeSpan.Zero
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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();