using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.Shared.DTOS.VisitaTecnica
{
    public class GET_VisitaTecnicaDTO
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = string.Empty;

        public int? ObraId { get; set; }

        public DateTime FechaVisita { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string Equipo { get; set; } = string.Empty;

        // Estos strings vienen del mapeo del Enum en el server para mostrar texto legible
        public string Tipo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        // Mantenemos el valor numérico/enum por si queremos pintar colores en el front
        public int EstadoValue { get; set; }
        public int TipoValue { get; set; }

        public string? Observaciones { get; set; }
        public decimal? CostoEstimado { get; set; }
    }
}
