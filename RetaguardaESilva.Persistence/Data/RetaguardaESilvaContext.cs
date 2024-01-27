using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RetaguardaESilva.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace RetaguardaESilva.Persistence.Data
{
    public class RetaguardaESilvaContext : DbContext
    {
        public RetaguardaESilvaContext(DbContextOptions<RetaguardaESilvaContext> options) : base(options){}        
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<EnderecoProduto> EnderecoProduto { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Permissao> Permissao { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoNota> PedidoNota { get; set; }
        public DbSet<NotaFiscal> NotaFiscal { get; set; }
        public DbSet<Transportador> Transportador { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<RetaguardaESilvaContext>
    {
        public RetaguardaESilvaContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Criando o DbContextOptionsBuilder manualmente
            var builder = new DbContextOptionsBuilder<RetaguardaESilvaContext>();
            // cria a connection string. 
            // requer a connectionstring no appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            // Cria o contexto
            return new RetaguardaESilvaContext(builder.Options);
        }        
    }
}
