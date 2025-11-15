using Zetta.Shared.DTOS.Presupuesto;
using Zetta.Shared.DTOS.Cliente;
using Zetta.Shared.DTOS.Obra;

namespace Zetta.Client.Servicios
{
    public class DashboardStatsDTO
    {
        public int TotalClientes { get; set; }
        public int TotalPresupuestos { get; set; }
        public int PresupuestosAceptados { get; set; }
        public int PresupuestosPendientes { get; set; }
        public int PresupuestosVencidos { get; set; }

        public int TotalObras { get; set; }
        public int ObrasIniciadas { get; set; }
        public int ObrasProceso { get; set; }
        public int ObrasFinalizadas { get; set; }

        public int TotalItems { get; set; }
    }

    public interface IEstadisticaService
    {
        Task<DashboardStatsDTO> GetDashboardStats();
    }
}
