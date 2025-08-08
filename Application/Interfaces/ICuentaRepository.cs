using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICuentaRepository : IGenericRepository<CuentaBancaria>
    {
        Task<CuentaBancaria?> GetByNumeroAsync(string numeroCuenta);              
        Task<CuentaBancaria?> GetWithTransaccionesByNumeroAsync(string numeroCuenta);
    }
}
