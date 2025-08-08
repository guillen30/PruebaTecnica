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
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(PruebaDbContext context) : base(context) { }

        public async Task<Cliente?> GetClienteConCuentasAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Cuentas)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
