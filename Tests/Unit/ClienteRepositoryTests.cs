using Application.Services;
using Domain.Entities;
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

    public class ClienteRepositoryTests
    {
        private readonly PruebaDbContext _context;
        private readonly ClienteRepository _clienteRepo;

        public ClienteRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<PruebaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new PruebaDbContext(options);
            _clienteRepo = new ClienteRepository(_context);
        }

        [Fact]
        public async Task CrearCliente_DeberiaPersistirEnBaseDeDatos()
        {
            var cliente = new Cliente
            {
                Nombre = "Daviana Paola",
                FechaNacimiento = new DateTime(1995, 5, 15),
                Sexo = "F",
                Ingresos = 1500
            };

            await _clienteRepo.AddAsync(cliente);
            await _clienteRepo.SaveChangesAsync();

         
            var clienteDb = await _clienteRepo.GetByIdAsync(cliente.Id);
            clienteDb.Should().NotBeNull();
            clienteDb!.Nombre.Should().Be("Daviana Paola");
        }

        [Fact]
        public async Task ObtenerClienteConCuentas_DeberiaIncluirCuentas()
        {
            var cliente = new Cliente
            {
                Nombre = "María José",
                FechaNacimiento = new DateTime(1998, 8, 20),
                Sexo = "F",
                Ingresos = 2000,
                Cuentas = new List<CuentaBancaria>
            {
                new CuentaBancaria { NumeroCuenta = "1001", SaldoInicial = 500, SaldoActual = 500 },
                new CuentaBancaria { NumeroCuenta = "1002", SaldoInicial = 1000, SaldoActual = 1000 }
            }
            };

            await _clienteRepo.AddAsync(cliente);
            await _clienteRepo.SaveChangesAsync();

            var clienteConCuentas = await _clienteRepo.GetClienteConCuentasAsync(cliente.Id);

            clienteConCuentas.Should().NotBeNull();
            clienteConCuentas!.Cuentas.Should().HaveCount(2);
            clienteConCuentas.Cuentas.Select(c => c.NumeroCuenta).Should().Contain("1001");
        }
    }
}
