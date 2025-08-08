using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly CuentaService _cuentaService;
        public CuentasController(CuentaService cuentaService) => _cuentaService = cuentaService;

        [HttpPost]
        public async Task<ActionResult<CuentaResponse>> CrearCuenta([FromBody] CrearCuentaRequest req)
        {
            var cuenta = await _cuentaService.CrearCuentaAsync(req.ClienteId, req.NumeroCuenta, req.SaldoInicial);
            if (cuenta is null) return BadRequest("No se pudo crear la cuenta.");

            var resp = new CuentaResponse(cuenta.Id, cuenta.NumeroCuenta, cuenta.SaldoActual);
            return CreatedAtAction(nameof(ConsultarSaldo), new { numeroCuenta = resp.NumeroCuenta }, resp);
        }

        [HttpGet("saldo/{numeroCuenta}")]
        public async Task<ActionResult<SaldoResponse>> ConsultarSaldo(string numeroCuenta)
        {
            var saldo = await _cuentaService.ConsultarSaldoAsync(numeroCuenta);
            return Ok(new SaldoResponse(numeroCuenta, saldo));
        }

        [HttpPost("depositar")]
        public async Task<IActionResult> Depositar([FromBody] MovimientoRequest req)
        {
            var ok = await _cuentaService.DepositarAsync(req.NumeroCuenta, req.Monto);
            return ok ? Ok() : BadRequest("Cuenta inexistente.");
        }

        [HttpPost("retirar")]
        public async Task<IActionResult> Retirar([FromBody] MovimientoRequest req)
        {
            var ok = await _cuentaService.RetirarAsync(req.NumeroCuenta, req.Monto);
            return ok ? Ok() : BadRequest("Fondos insuficientes o cuenta inexistente.");
        }

        [HttpGet("historial/{numeroCuenta}")]
        public async Task<ActionResult<HistorialResponse>> Historial(string numeroCuenta)
        {
            var items = (await _cuentaService.ObtenerHistorialAsync(numeroCuenta))
                       .Select(t => new TransaccionItem(t.Id, t.Tipo.ToString(), t.Monto, t.Fecha, t.SaldoDespues));
            return Ok(new HistorialResponse(numeroCuenta, items));
        }
    }
}
