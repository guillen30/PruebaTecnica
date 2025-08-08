using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CuentaService
    {
        private readonly ICuentaRepository _cuentaRepo;
        private readonly ITransaccionRepository _transaccionRepo;

        public CuentaService(ICuentaRepository cuentaRepo, ITransaccionRepository transaccionRepo)
        {
            _cuentaRepo = cuentaRepo;
            _transaccionRepo = transaccionRepo;
        }

        public async Task<CuentaBancaria?> CrearCuentaAsync(int clienteId, string numeroCuenta, decimal saldoInicial)
        {
            if (saldoInicial <= 0m)
                throw new ArgumentException("El saldo inicial debe ser mayor a 0.", nameof(saldoInicial));
            var cuenta = new CuentaBancaria
            {
                ClienteId = clienteId,
                NumeroCuenta = numeroCuenta,
                SaldoInicial = saldoInicial,
                SaldoActual = saldoInicial
            };

            await _cuentaRepo.AddAsync(cuenta);
            await _cuentaRepo.SaveChangesAsync();
            return cuenta;
        }

        public async Task<decimal> ConsultarSaldoAsync(string numeroCuenta)
        {
            var cuenta = await _cuentaRepo.GetByNumeroAsync(numeroCuenta);
            return cuenta?.SaldoActual ?? 0m;
        }

        public async Task<bool> DepositarAsync(string numeroCuenta, decimal monto)
        {
            var cuenta = await _cuentaRepo.GetByNumeroAsync(numeroCuenta);
            if (cuenta is null) return false;

            cuenta.SaldoActual += monto;

            var transaccion = new Transaccion
            {
                Tipo = TipoTransaccion.Deposito,
                Monto = monto,
                SaldoDespues = cuenta.SaldoActual,
                CuentaBancariaId = cuenta.Id
            };

            await _transaccionRepo.AddAsync(transaccion);
            _cuentaRepo.Update(cuenta);
            await _transaccionRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RetirarAsync(string numeroCuenta, decimal monto)
        {
            var cuenta = await _cuentaRepo.GetByNumeroAsync(numeroCuenta);
            if (cuenta is null || cuenta.SaldoActual < monto) return false;

            cuenta.SaldoActual -= monto;

            var transaccion = new Transaccion
            {
                Tipo = TipoTransaccion.Retiro,
                Monto = monto,
                SaldoDespues = cuenta.SaldoActual,
                CuentaBancariaId = cuenta.Id
            };

            await _transaccionRepo.AddAsync(transaccion);
            _cuentaRepo.Update(cuenta);
            await _transaccionRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Transaccion>> ObtenerHistorialAsync(string numeroCuenta)
        {
            var cuenta = await _cuentaRepo.GetWithTransaccionesByNumeroAsync(numeroCuenta);
            return cuenta?.Transacciones.OrderBy(t => t.Fecha) ?? Enumerable.Empty<Transaccion>();
        }
    }
}
