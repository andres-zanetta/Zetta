using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.Shared.DTOS.VisitaTecnica
{
    public class PUT_VisitaTecnicaDTO
    {
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        public int? ObraId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime FechaVisita { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe especificar el equipo o motivo.")]
        public string Equipo { get; set; } = string.Empty;

        public int Tipo { get; set; }

        public int Estado { get; set; } // Aquí cambiamos de Pendiente a Completada

        public string? Observaciones { get; set; }

        public decimal? CostoEstimado { get; set; }
    }
}
