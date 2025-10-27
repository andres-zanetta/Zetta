using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zetta.BD.DATA.ENTITY;
using Zetta.Shared.DTOS.PresItemDetalle;

namespace Zetta.Shared.DTOS.Presupuesto
{
    public class GET_PresupuestoDTO
    {
        public int Id { get; set; }

        public bool Aceptado { get; set; } = false; // Estado de aceptación del presupuesto por el cliente.

        public string? Observacion { get; set; }

        public decimal Total { get; set; }

        public decimal? ManodeObra { get; set; } = 0.00m;

        public decimal TotalP { get; set; } = 0.00m;

        public string TiempoAproxObra { get; set; } = "0";

        public string ValidacionDias { get; set; } = "30";

        public OpcionDePago OpcionDePago { get; set; }

        public Rubro Rubro { get; set; }
        public bool Materiales { get; set; }

        // --- PROPIEDAD AGREGADA ---
        // Necesaria para mostrar el nombre en la tabla de Presupuesto.razor
        public string? NombreCliente { get; set; }

        public int ClienteId { get; set; } // ID del cliente asociado
        public List<GET_PresItemDetalleDTO>? ItemsDetalle { get; set; }

    }
}