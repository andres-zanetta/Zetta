using System;

namespace Zetta.Shared.DTOS.PresItemDetalle
{
    public class GET_PresItemDetalleDTO
    {
        public int Id { get; set; }
        public int PresupuestoId { get; set; }
        public int ItemPresupuestoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string? NombreItem { get; set; }
        public string? DescripcionItem { get; set; }
        public DateTime? FechaActPrecioItem { get; set; }

        // --- CAMPO AGREGADO ---
        public string? MedidaItem { get; set; } // Unidad de medida del ítem (ej: 'm', 'u', 'kg')
    }
}