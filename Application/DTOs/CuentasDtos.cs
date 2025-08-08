using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{

    public record CrearCuentaRequest(int ClienteId, string NumeroCuenta, decimal SaldoInicial);
    public record CuentaResponse(int Id, string NumeroCuenta, decimal SaldoActual);

    public record MovimientoRequest(string NumeroCuenta, decimal Monto);
    public record SaldoResponse(string NumeroCuenta, decimal SaldoActual);

    public record TransaccionItem(int Id, string Tipo, decimal Monto, DateTime Fecha, decimal SaldoDespues);
    public record HistorialResponse(string NumeroCuenta, IEnumerable<TransaccionItem> Transacciones);
}
