using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transaccion
    {
        public int Id { get; set; }
        public TipoTransaccion Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public decimal SaldoDespues { get; set; }

        public int CuentaBancariaId { get; set; }
        public CuentaBancaria CuentaBancaria { get; set; } = null!;
    }
}
