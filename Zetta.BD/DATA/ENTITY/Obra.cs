using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.BD.DATA.ENTITY
{
    public class Obra : EntityBase
    {
        public EstadoObra EstadoObra { get; set; } // Iniciada, En Proceso, Finalizada

        public int PresupuestoId { get; set; }
        public Presupuesto Presupuesto { get; set; }

        public DateTime FechaInicio { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public bool? MaterialesCompradosPorUsuario { get; set; } // true=comprado, false=no, null=pendiente
        public bool? MaterialesEntregados { get; set; } // true=entregado, false=no, null=pendiente

    }

    public enum EstadoObra
    {
        Iniciada,
        EnProceso,
        Finalizada
    }

    //    //Diccionario 
    //| Nombre          | Tipo       | Descripción                                      |
    //| --------------- | ---------- | ------------------------------------------------ |
    //| `Estado`        | `string`   | Estado actual: iniciada, en proceso, finalizada. |
    //| `PresupuestoId` | `int`      | Presupuesto relacionado.                         |
    //| `ProfesionalId` | `int?`     | Profesional asignado (opcional).                 |
    //| `FechaInicio`   | `DateTime` | Fecha de comienzo de la obra.                    |
    //| `FechaFin`      | `DateTime` | Fecha de finalización.                           |

}
