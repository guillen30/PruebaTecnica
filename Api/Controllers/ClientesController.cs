using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using static Application.DTOs.ClientesDtos;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepo;

        public ClientesController(IClienteRepository clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        [HttpPost]
        public async Task<ActionResult<ClienteResponse>> CrearCliente([FromBody] CrearClienteRequest request)
        {
            var cliente = new Cliente
            {
                Nombre = request.Nombre,
                FechaNacimiento = request.FechaNacimiento,
                Sexo = request.Sexo,
                Ingresos = request.Ingresos
            };

            await _clienteRepo.AddAsync(cliente);
            await _clienteRepo.SaveChangesAsync();

            var response = new ClienteResponse(cliente.Id, cliente.Nombre, cliente.FechaNacimiento, cliente.Sexo, cliente.Ingresos);
            return CreatedAtAction(nameof(ObtenerCliente), new { id = cliente.Id }, response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteResponse>> ObtenerCliente(int id)
        {
            var cliente = await _clienteRepo.GetByIdAsync(id);
            if (cliente is null) return NotFound();

            return Ok(new ClienteResponse(cliente.Id, cliente.Nombre, cliente.FechaNacimiento, cliente.Sexo, cliente.Ingresos));
        }
    }
}
