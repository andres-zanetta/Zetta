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
        public bool Aceptado { get; set; } = false;
        public string? Observacion { get; set; }
        public decimal Total { get; set; }
        public decimal? ManodeObra { get; set; } = 0.00m;
        public decimal TotalP { get; set; } = 0.00m;
        public string TiempoAproxObra { get; set; } = "0";
        public string ValidacionDias { get; set; } = "30";
        public OpcionDePago OpcionDePago { get; set; } // El enum
        public Rubro Rubro { get; set; } // El enum
        public bool Materiales { get; set; }
        public int ClienteId { get; set; }

        // --- CAMPOS AGREGADOS ---
        public string? NombreCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public string? TelefonoCliente { get; set; }
        public string? EmailCliente { get; set; }
        public string? LocalidadCliente { get; set; }
        public string? RubroNombre { get; set; } // Nombre legible del rubro

        public List<GET_PresItemDetalleDTO>? ItemsDetalle { get; set; }
    }
}