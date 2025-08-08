using Application.Services;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Unit
{
    public class CuentaServiceTests
    {
        private readonly PruebaDbContext _context;
        private readonly CuentaService _service;

        public CuentaServiceTests()
        {
            var options = new DbContextOptionsBuilder<PruebaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new PruebaDbContext(options);

            var cuentaRepo = new CuentaRepository(_context);
            var transaccionRepo = new TransaccionRepository(_context);

            _service = new CuentaService(cuentaRepo, transaccionRepo);
        }

        [Fact]
        public async Task CrearCuenta_DeberiaCrearCuentaCorrectamente()
        {
            int clienteId = 1;
            string numeroCuenta = "0001";
            decimal saldoInicial = 1000;

            var cuenta = await _service.CrearCuentaAsync(clienteId, numeroCuenta, saldoInicial);


            cuenta.Should().NotBeNull();
            cuenta!.NumeroCuenta.Should().Be("0001");
            cuenta.SaldoActual.Should().Be(1000);
        }

        [Fact]
        public async Task Depositar_DeberiaIncrementarSaldo()
        {

            var cuenta = await _service.CrearCuentaAsync(1, "0002", 500);


            bool result = await _service.DepositarAsync("0002", 200);

            result.Should().BeTrue();
            var saldo = await _service.ConsultarSaldoAsync("0002");
            saldo.Should().Be(700);
        }

        [Fact]
        public async Task Retirar_DeberiaDisminuirSaldoCuandoHayFondos()
        {
            var cuenta = await _service.CrearCuentaAsync(1, "0003", 500);


            bool result = await _service.RetirarAsync("0003", 200);

    
            result.Should().BeTrue();
            var saldo = await _service.ConsultarSaldoAsync("0003");
            saldo.Should().Be(300);
        }

        [Fact]
        public async Task Retirar_NoDebePermitirCuandoFondosInsuficientes()
        {
            var cuenta = await _service.CrearCuentaAsync(1, "0004", 100);

            bool result = await _service.RetirarAsync("0004", 200);

            result.Should().BeFalse();
            var saldo = await _service.ConsultarSaldoAsync("0004");
            saldo.Should().Be(100);
        }

        [Fact]
        public async Task Historial_DeberiaRetornarTransaccionesEnOrden()
        {
      
            var cuenta = await _service.CrearCuentaAsync(1, "0005", 100);
            await _service.DepositarAsync("0005", 100);
            await _service.RetirarAsync("0005", 50);

            var historial = await _service.ObtenerHistorialAsync("0005");

            historial.Should().HaveCount(2);
            historial.First().Monto.Should().Be(100);
            historial.Last().Monto.Should().Be(50);
        }

        [Fact]
        public async Task CrearCuenta_DeberiaFallar_CuandoSaldoInicial_NoEsMayorCero()
        {
            Func<Task> actZero = async () => await _service.CrearCuentaAsync(1, "ZCERO1", 0m);
            Func<Task> actNeg = async () => await _service.CrearCuentaAsync(1, "ZNEG01", -1m);

            await actZero.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*mayor a 0*");
            await actNeg.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*mayor a 0*");
        }
    }
}
