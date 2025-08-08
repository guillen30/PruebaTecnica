using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ClientesDtos
    {
        public record CrearClienteRequest(string Nombre, DateTime FechaNacimiento, string Sexo, decimal Ingresos);
        public record ClienteResponse(int Id, string Nombre, DateTime FechaNacimiento, string Sexo, decimal Ingresos);
    }
}
