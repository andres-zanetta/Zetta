using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.Shared.DTOS.Obra
{
    public class PUT_ObraDTO
    {
        public string EstadoObra { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public int ClienteId { get; set; }
        public int PresupuestoId { get; set; }
        public List<string>? Comentarios { get; set; }
        public bool? MaterialesCompradosPorUsuario { get; set; }
        public bool? MaterialesEntregados { get; set; }
    }
}
