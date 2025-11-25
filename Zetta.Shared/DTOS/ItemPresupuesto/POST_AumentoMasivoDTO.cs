using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.Shared.DTOS.ItemPresupuesto
{
    public class POST_AumentoMasivoDTO
    {
        public string? Marca { get; set; }

        [Required]
        [Range(-99, 1000, ErrorMessage = "Porcentaje inválido")]
        public decimal Porcentaje { get; set; } // Ej: 10 para 10%, 15.5 para 15.5%
    }
}
