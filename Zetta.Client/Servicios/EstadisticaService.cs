using Zetta.Shared.DTOS.Cliente;
using Zetta.Shared.DTOS.ItemPresupuesto;
using Zetta.Shared.DTOS.Obra;
using Zetta.Shared.DTOS.Presupuesto;

namespace Zetta.Client.Servicios
{
    public class EstadisticaService : IEstadisticaService
    {
        private readonly IClienteService _clienteService;
        private readonly IPresupuestoServices _presupuestoService;
        private readonly IObraService _obraService;
        private readonly IItemPresupuestoService _itemService;

        public EstadisticaService(
            IClienteService clienteService,
            IPresupuestoServices presupuestoService,
            IObraService obraService,
            IItemPresupuestoService itemService)
        {
            _clienteService = clienteService;
            _presupuestoService = presupuestoService;
            _obraService = obraService;
            _itemService = itemService;
        }

        public async Task<DashboardStatsDTO> GetDashboardStats()
        {
            var clientes = await _clienteService.GetAll() ?? new List<GET_ClienteDTO>();
            var presupuestos = await _presupuestoService.GetAll() ?? new List<GET_PresupuestoDTO>();
            var obras = await _obraService.GetAllAsync() ?? new List<GET_ObraDTO>();
            var items = await _itemService.GetAll() ?? new List<GET_ItemPresupuestoDTO>();

            var stats = new DashboardStatsDTO
            {
                TotalClientes = clientes.Count,
                TotalPresupuestos = presupuestos.Count,
                TotalItems = items.Count,
                TotalObras = obras.Count
            };

            // Presupuestos
            stats.PresupuestosAceptados = presupuestos.Count(p => p.Aceptado);

            int vencidos = 0;
            int pendientes = 0;

            foreach (var p in presupuestos)
            {
                if (p.Aceptado)
                    continue;

                bool esVencido = false;

                if (int.TryParse(p.ValidacionDias, out var dias))
                {
                    var fechaVencimiento = p.FechaCreacion.Date.AddDays(dias);
                    if (DateTime.Today > fechaVencimiento)
                        esVencido = true;
                }

                if (esVencido)
                    vencidos++;
                else
                    pendientes++;
            }

            stats.PresupuestosVencidos = vencidos;
            stats.PresupuestosPendientes = pendientes;

            // Obras (normalizo el texto para evitar problemas con espacios o mayúsculas)
            stats.ObrasIniciadas = obras.Count(o =>
                (o.EstadoObra ?? string.Empty)
                    .Trim().Equals("Iniciada", StringComparison.OrdinalIgnoreCase));

            stats.ObrasProceso = obras.Count(o =>
            {
                var estado = (o.EstadoObra ?? string.Empty).ToLowerInvariant().Replace(" ", "");
                return estado == "enproceso";
            });

            stats.ObrasFinalizadas = obras.Count(o =>
                (o.EstadoObra ?? string.Empty)
                    .Trim().Equals("Finalizada", StringComparison.OrdinalIgnoreCase));

            return stats;
        }
    }
}

