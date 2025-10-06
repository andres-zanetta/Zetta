using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zetta.BD.DATA.ENTITY;

namespace Zetta.Shared.DTOS.Presupuesto
{
    public class PUT_PresupuestoDTO
    {
        public int Id { get; set; }
        public Rubro Rubro { get; set; }
        public bool Aceptado { get; set; }
        public string? Observacion { get; set; }
        public decimal? ManodeObra { get; set; }
        public decimal Total { get; set; }
        public decimal TotalP { get; set; }
        public string TiempoAproxObra { get; set; }
        public string ValidacionDias { get; set; }
        public OpcionDePago OpcionDePago { get; set; }
        public int ClienteId { get; set; }
        public List<PUT_PresItemDetalleDTO>? ItemsDetalle { get; set; }
    }
}
