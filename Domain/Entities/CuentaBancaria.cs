using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Domain.Entities
{
    public class CuentaBancaria
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public decimal SaldoInicial { get; set; }
        public decimal SaldoActual { get; set; }


        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;


        public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
    }
}
