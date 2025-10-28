using AutoMapper;
using Zetta.BD.DATA.ENTITY;
using Zetta.Shared.DTOS.Cliente;
using Zetta.Shared.DTOS.ItemPresupuesto;
using Zetta.Shared.DTOS.Obra;
using Zetta.Shared.DTOS.PresItemDetalle;
using Zetta.Shared.DTOS.Presupuesto;

namespace Zetta.Server.Util // O tu namespace correcto
{
    public class AutoMapperProfiles : Profile
    {
        // Función helper para obtener el nombre del rubro
        private string GetNombreRubro(Rubro rubro) => rubro switch
        {
            Rubro.Gas => "Gas",
            Rubro.Electricidad => "Electricidad",
            Rubro.Refrigeracion => "Refrigeración",
            Rubro.Solar => "Solar",
            Rubro.Plomeria => "Plomería",
            _ => rubro.ToString() // Valor por defecto si se añade otro rubro
        };

        public AutoMapperProfiles()
        {
            // --- Mapeos de Cliente (Sin cambios) ---
            CreateMap<Cliente, GET_ClienteDTO>();
            CreateMap<POST_ClienteDTO, Cliente>();
            CreateMap<PUT_ClienteDTO, Cliente>();

            // --- Mapeos de Presupuesto ---
            // GET: Actualizado con todos los campos
            CreateMap<Presupuesto, GET_PresupuestoDTO>()
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente != null ? $"{src.Cliente.Nombre} {src.Cliente.Apellido}" : null)) // Nombre y Apellido
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.ItemsDetalle, opt => opt.MapFrom(src => src.ItemsDetalle))
                .ForMember(dest => dest.DireccionCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Direccion : null)) // Nuevo
                .ForMember(dest => dest.TelefonoCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Telefono : null)) // Nuevo
                .ForMember(dest => dest.EmailCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Email : null))       // Nuevo
                .ForMember(dest => dest.LocalidadCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Localidad : null)) // Nuevo
                .ForMember(dest => dest.RubroNombre, opt => opt.MapFrom(src => GetNombreRubro(src.Rubro))); // Nuevo - Usa la función helper

            // POST: (Sin cambios relevantes aquí, asegúrate de que mapee lo necesario)
            CreateMap<POST_PresupuestoDTO, Presupuesto>()
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.ItemsDetalle, opt => opt.MapFrom(src => src.ItemsDetalle));

            // PUT: (Sin cambios relevantes aquí, asegúrate de que mapee lo necesario)
            CreateMap<PUT_PresupuestoDTO, Presupuesto>()
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.ItemsDetalle, opt => opt.MapFrom(src => src.ItemsDetalle));


            // --- Mapeos de PresItemDetalle ---
            // Entidad a GET DTO: Actualizado con nuevos campos
            CreateMap<PresItemDetalle, GET_PresItemDetalleDTO>()
                .ForMember(dest => dest.NombreItem, opt => opt.MapFrom(src => src.ItemPresupuesto != null ? src.ItemPresupuesto.Nombre : null))
                // --- REGLAS AGREGADAS ---
                .ForMember(dest => dest.DescripcionItem, opt => opt.MapFrom(src => src.ItemPresupuesto != null ? src.ItemPresupuesto.Descripcion : null))
                .ForMember(dest => dest.FechaActPrecioItem, opt => opt.MapFrom(src => src.ItemPresupuesto != null ? src.ItemPresupuesto.FechActuPrecio : null))
                .ForMember(dest => dest.MedidaItem, opt => opt.MapFrom(src => src.ItemPresupuesto != null ? src.ItemPresupuesto.Medida : null));

            // DTO a Entidad (Sin cambios)
            CreateMap<POST_PresItemDetalleDTO, PresItemDetalle>()
                .ForMember(dest => dest.ItemPresupuesto, opt => opt.Ignore())
                .ForMember(dest => dest.Presupuesto, opt => opt.Ignore());
            CreateMap<PUT_PresItemDetalleDTO, PresItemDetalle>()
                .ForMember(dest => dest.ItemPresupuesto, opt => opt.Ignore())
                .ForMember(dest => dest.Presupuesto, opt => opt.Ignore());


            // --- Mapeos de ItemPresupuesto (Sin cambios) ---
            CreateMap<ItemPresupuesto, GET_ItemPresupuestoDTO>();
            CreateMap<POST_ItemPresupuestoDTO, ItemPresupuesto>();
            CreateMap<PUT_ItemPresupuestoDTO, ItemPresupuesto>();

            // --- Mapeos de Obra (Sin cambios) ---
            CreateMap<Obra, GET_ObraDTO>()
               .ForMember(dest => dest.EstadoObra, opt => opt.MapFrom(src => src.EstadoObra.ToString()))
               .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.Cliente.Id))
               .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente.Nombre));

            CreateMap<POST_ObraDTO, Obra>()
                .ForMember(dest => dest.EstadoObra, opt => opt.MapFrom(src => Enum.Parse<EstadoObra>(src.EstadoObra)))
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Presupuesto, opt => opt.Ignore())
                .ForMember(dest => dest.PresupuestoId, opt => opt.MapFrom(src => src.PresupuestoId))
                .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => src.FechaInicio));

            CreateMap<PUT_ObraDTO, Obra>()
                .ForMember(dest => dest.EstadoObra, opt => opt.MapFrom(src => Enum.Parse<EstadoObra>(src.EstadoObra)))
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Presupuesto, opt => opt.Ignore())
                .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => src.FechaInicio));
        }
    }
}