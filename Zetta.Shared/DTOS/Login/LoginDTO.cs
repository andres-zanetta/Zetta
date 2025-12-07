using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.Shared.DTOS
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string NombreU { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio para ingresar")]
        public int DNI { get; set; }
    }

    public class SesionDTO
    {
        public string Token { get; set; }
        public string NombreU { get; set; }
    }
}
