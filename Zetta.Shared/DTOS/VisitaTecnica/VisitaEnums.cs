using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.Shared.DTOS.VisitaTecnica
{ 
    public enum EstadoVisitaDTO
    {
        Pendiente = 0,
        Completada = 1,
        Cancelada = 2,
        Reprogramada = 3
    }

    public enum TipoVisitaDTO
    {
        Mantenimiento = 0,
        Reparacion = 1,
        Relevamiento = 2,
        Instalacion = 3
    }
}
