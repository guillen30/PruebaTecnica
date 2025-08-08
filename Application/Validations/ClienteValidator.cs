using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations
{

    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.FechaNacimiento).LessThan(DateTime.Today);
            RuleFor(x => x.Sexo).NotEmpty().Must(x => x == "M" || x == "F");
            RuleFor(x => x.Ingresos).GreaterThanOrEqualTo(0);
        }
    }
}
