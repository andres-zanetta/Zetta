using Zetta.Shared.DTOS.VisitaTecnica;

namespace Zetta.Client.Servicios
{
    public interface IVisitaTecnicaService
    {
        // Obtener todas las visitas para la grilla
        Task<List<GET_VisitaTecnicaDTO>?> GetAll();

        // Obtener una visita específica para editar
        Task<GET_VisitaTecnicaDTO?> GetById(int id);

        // Crear una nueva visita (devuelve el ID generado)
        Task<int> Create(POST_VisitaTecnicaDTO visita);

        // Actualizar una visita existente
        Task Update(int id, PUT_VisitaTecnicaDTO visita);

        // Eliminar una visita
        Task Delete(int id);
    }
}