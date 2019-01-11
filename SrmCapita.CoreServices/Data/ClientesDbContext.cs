using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SrmCapita.CoreServices.Model;

namespace SrmCapita.CoreServices.Data
{
    public class ClientesDbContext : DbContext
    {
        public ClientesDbContext(DbContextOptions<ClientesDbContext> options) : base(options) { }

        public virtual DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nome)
                    .HasColumnName("Nome")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("Email")
                    .HasMaxLength(255)
                    .IsUnicode(false);


                entity.Property(e => e.Telefone)
                    .HasColumnName("Fone")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DataCadastro)
                    .HasColumnName("Data")
                    .HasColumnType("datetime");

                entity.Property(e => e.LimiteCompra)
                    .HasColumnName("Limite")
                    .HasColumnType("decimal");


            });
          
        }
    }
}
