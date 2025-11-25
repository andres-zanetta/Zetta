using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.BD.DATA.ENTITY
{
    public class VisitaTecnica : EntityBase
    {
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        //Si el mantenimiento corresponde a una obra previa
        public int? ObraId { get; set; }
        public Obra? Obra { get; set; }

        [Required]
        public DateTime FechaVisita { get; set; }

        [Required]
        public string Direccion { get; set; } // La dirección del equipo a mantener

        [Required]
        public string Equipo { get; set; } // Ej: "Aire Acondicionado Samsung Inverter"

        public TipoVisitaTecnica Tipo { get; set; } = TipoVisitaTecnica.Mantenimiento;

        public EstadoVisita Estado { get; set; } = EstadoVisita.Pendiente;

        public string? Observaciones { get; set; } // Diagnostico

        [Precision(18, 2)]
        public decimal? CostoEstimado { get; set; }
    }

    public enum EstadoVisita
    {
        Pendiente = 0,
        Completada = 1,
        Cancelada = 2,
        Reprogramada = 3
    }

    public enum TipoVisitaTecnica
    {
        Mantenimiento = 0,
        Reparacion = 1,
        Relevamiento = 2, // Para ir a ver antes de presupuestar
        Instalacion = 3
    }
}
