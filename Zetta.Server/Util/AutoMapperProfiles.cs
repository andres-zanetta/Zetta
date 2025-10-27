using AutoMapper;
using Zetta.BD.DATA.ENTITY;
using Zetta.Shared.DTOS.Cliente;
using Zetta.Shared.DTOS.ItemPresupuesto;
using Zetta.Shared.DTOS.Obra;
using Zetta.Shared.DTOS.PresItemDetalle;
using Zetta.Shared.DTOS.Presupuesto;

namespace Zetta.Server.Util // 
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cliente, GET_ClienteDTO>();
            CreateMap<POST_ClienteDTO, Cliente>();
            CreateMap<PUT_ClienteDTO, Cliente>();

            // --- Mapeos de Presupuesto ---
            CreateMap<Presupuesto, GET_PresupuestoDTO>()
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Nombre : null))
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.ItemsDetalle, opt => opt.MapFrom(src => src.ItemsDetalle)); // AutoMapper maneja List<Entidad> a List<DTO> si existe el mapeo del item

            // POST: Asegura que los detalles anidados se mapeen
            CreateMap<POST_PresupuestoDTO, Presupuesto>()
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.ItemsDetalle, opt => opt.MapFrom(src => src.ItemsDetalle));

            // PUT: Asegura que los detalles anidados se mapeen
            CreateMap<PUT_PresupuestoDTO, Presupuesto>()
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.ItemsDetalle, opt => opt.MapFrom(src => src.ItemsDetalle));


            // --- Mapeos de PresItemDetalle ---
            // * NUEVO MAPEO * : Entidad a GET DTO
            CreateMap<PresItemDetalle, GET_PresItemDetalleDTO>();

            // DTO a Entidad (Mapeos existentes - Verifica nombres de propiedades)
            CreateMap<POST_PresItemDetalleDTO, PresItemDetalle>()
                .ForMember(dest => dest.ItemPresupuesto, opt => opt.Ignore())
                .ForMember(dest => dest.Presupuesto, opt => opt.Ignore());

            CreateMap<PUT_PresItemDetalleDTO, PresItemDetalle>()
                .ForMember(dest => dest.ItemPresupuesto, opt => opt.Ignore())
                .ForMember(dest => dest.Presupuesto, opt => opt.Ignore());


            // --- Mapeos de ItemPresupuesto (No se necesitan cambios) ---
            CreateMap<ItemPresupuesto, GET_ItemPresupuestoDTO>();
            CreateMap<POST_ItemPresupuestoDTO, ItemPresupuesto>();
            CreateMap<PUT_ItemPresupuestoDTO, ItemPresupuesto>();

            // --- Mapeos de Obra (No se necesitan cambios) ---
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