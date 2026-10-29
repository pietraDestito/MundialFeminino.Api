
using Microsoft.EntityFrameworkCore;
using MundialFeminino.Api.Models;

namespace MundialFeminino.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Edicao> Edicoes { get; set; }
        public DbSet<GolJogadora> GolsJogadoras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<GolJogadora>(entity =>
            {
                entity.Property(g => g.Nome)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(g => g.Selecao)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasIndex(g => new { g.Nome, g.Selecao, g.Ano })
                    .IsUnique();
            });


            modelBuilder.Entity<Edicao>().HasData(
                new Edicao
                {
                    Id = 1,
                    Ano = 1991,
                    PaisSede = "China",
                    Campeao = "Estados Unidos",
                    ViceCampeao = "Noruega"
                },
                new Edicao
                {
                    Id = 2,
                    Ano = 1995,
                    PaisSede = "Suécia",
                    Campeao = "Noruega",
                    ViceCampeao = "Alemanha"
                },
                new Edicao
                {
                    Id = 3,
                    Ano = 1999,
                    PaisSede = "Estados Unidos",
                    Campeao = "Estados Unidos",
                    ViceCampeao = "China"
                },
                new Edicao
                {
                    Id = 4,
                    Ano = 2003,
                    PaisSede = "Estados Unidos",
                    Campeao = "Alemanha",
                    ViceCampeao = "Suécia"
                },
                new Edicao
                {
                    Id = 5,
                    Ano = 2007,
                    PaisSede = "China",
                    Campeao = "Alemanha",
                    ViceCampeao = "Brasil"
                },
                new Edicao
                {
                    Id = 6,
                    Ano = 2011,
                    PaisSede = "Alemanha",
                    Campeao = "Japão",
                    ViceCampeao = "Estados Unidos"
                },
                new Edicao
                {
                    Id = 7,
                    Ano = 2015,
                    PaisSede = "Canadá",
                    Campeao = "Estados Unidos",
                    ViceCampeao = "Japão"
                },
                new Edicao
                {
                    Id = 8,
                    Ano = 2019,
                    PaisSede = "França",
                    Campeao = "Estados Unidos",
                    ViceCampeao = "Países Baixos"
                },
                new Edicao
                {
                    Id = 9,
                    Ano = 2023,
                    PaisSede = "Austrália e Nova Zelândia",
                    Campeao = "Espanha",
                    ViceCampeao = "Inglaterra"
                }
            );
        }
    }
}
