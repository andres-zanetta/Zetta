using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.Shared.DTOS.PresItemDetalle
{
    public class GET_PresItemDetalleDTO
    {
        public int Id { get; set; }
        public int PresupuestoId { get; set; }
        public int ItemPresupuestoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
