using GrupoColorado.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrupoColorado.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<TipoTelefone> TiposTelefone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasKey(c => c.CodigoCliente);
            modelBuilder.Entity<TipoTelefone>().HasKey(t => t.CodigoTipoTelefone);
            modelBuilder.Entity<Telefone>().HasKey(t => new { t.CodigoCliente, t.NumeroTelefone });

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Telefones)
                .WithOne()
                .HasForeignKey(t => t.CodigoCliente);

            modelBuilder.Entity<Telefone>()
                .HasOne<TipoTelefone>()
                .WithMany()
                .HasForeignKey(t => t.CodigoTipoTelefone);

            modelBuilder.Entity<TipoTelefone>().HasData(
                new TipoTelefone { CodigoTipoTelefone = 1, DescricaoTipoTelefone = "Residencial", DataInsercao = DateTime.UtcNow, UsuarioInsercao = "sistema" },
                new TipoTelefone { CodigoTipoTelefone = 2, DescricaoTipoTelefone = "Comercial", DataInsercao = DateTime.UtcNow, UsuarioInsercao = "sistema" },
                new TipoTelefone { CodigoTipoTelefone = 3, DescricaoTipoTelefone = "WhatsApp", DataInsercao = DateTime.UtcNow, UsuarioInsercao = "sistema" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}