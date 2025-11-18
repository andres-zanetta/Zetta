using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zetta.BD.DATA.ENTITY;

namespace Zetta.Shared.DTOS.ItemPresupuesto
{
    public class POST_ItemPresupuestoDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
        [Range(0.1, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; set; }
        public string? Medida { get; set; }
        public string? Material { get; set; }
        public string? Descripcion { get; set; }
        public string? Fabricante { get; set; }
        public string? Marca { get; set; }
        public DateTime? FechActuPrecio { get; set; }
    }
}
