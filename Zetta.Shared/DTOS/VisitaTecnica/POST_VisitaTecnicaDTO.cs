using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.Shared.DTOS.VisitaTecnica
{
    public class POST_VisitaTecnicaDTO
    {
        [Required(ErrorMessage = "Debe seleccionar un cliente.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un cliente válido.")]
        public int ClienteId { get; set; }

        public int? ObraId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime FechaVisita { get; set; } = DateTime.Now.AddDays(1);

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe especificar el equipo o motivo.")]
        public string Equipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de visita es obligatorio.")]
        public int Tipo { get; set; } // Se mapeará a TipoVisitaDTO

        public string? Observaciones { get; set; }

        public decimal? CostoEstimado { get; set; }
    }
}
