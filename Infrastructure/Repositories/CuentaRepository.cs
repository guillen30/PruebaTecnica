using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CuentaRepository : GenericRepository<CuentaBancaria>, ICuentaRepository
    {
        public CuentaRepository(PruebaDbContext context) : base(context) { }

        public async Task<CuentaBancaria?> GetByNumeroAsync(string numeroCuenta)
            => await _context.CuentasBancarias
        .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);

        public async Task<CuentaBancaria?> GetWithTransaccionesByNumeroAsync(string numeroCuenta)
            => await _context.CuentasBancarias
                .AsNoTracking() 
                .Include(c => c.Transacciones)
                .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);
    }
}
