using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PruebaDbContext : DbContext
    {
        public PruebaDbContext(DbContextOptions<PruebaDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<CuentaBancaria> CuentasBancarias => Set<CuentaBancaria>();
        public DbSet<Transaccion> Transacciones => Set<Transaccion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Cuentas)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CuentaBancaria>()
                .HasMany(c => c.Transacciones)
                .WithOne(t => t.CuentaBancaria)
                .HasForeignKey(t => t.CuentaBancariaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CuentaBancaria>()
                .HasIndex(c => c.NumeroCuenta)
                .IsUnique();
        }
    }
}
