🧰 Zettium Servicios

Zettium es un sistema web para la gestión integral de:

Presupuestos técnicos

Obras y su seguimiento

Visitas técnicas

Catálogo de ítems (materiales/servicios)

Clientes y su historial de trabajo 

Está pensado para rubros como gas, electricidad, refrigeración, energía solar, plomería, etc., con foco en eficiencia, trazabilidad y facilidad de uso.

👥 Autores

Andrés Zanetta

Leonardo Contreras

🎯 Objetivo del sistema

Centralizar en una sola herramienta la gestión de clientes, presupuestos, obras y visitas.

Reducir errores administrativos automatizando cálculos (subtotales, totales, tiempos estimados). 


Brindar una visión clara del estado del negocio: presupuestos aceptados/rechazados, obras activas, visitas pendientes.

Mejorar la comunicación con el cliente mediante presupuestos claros en PDF y seguimiento del estado de la obra. 


🧩 Funcionalidades principales
1. Gestión de Clientes

ABM completo (alta, edición, baja lógica).

Filtros por nombre/apellido y localidad con búsqueda en vivo. 


Papelera de clientes:

Restaurar registros eliminados por error.

Eliminación definitiva desde la papelera. 

2. Catálogo de Ítems

ABM de ítems base (materiales/servicios).

Campos: nombre, precio unitario, medida, descripción, fabricante, marca y fecha de actualización de precio. 


Filtros dinámicos:

Búsqueda por nombre/descripcion.

Filtro por fabricante.

Filtro por marca. 


Actualización masiva de precios por marca o “todas las marcas” con porcentaje positivo/negativo. 


Papelera de ítems con restauración/eliminación definitiva.

3. Gestión de Presupuestos

Creación y edición de presupuestos con:

Cliente asociado.

Rubro (gas, refrigeración, electricidad, etc.).

Opción de pago.

Si incluye o no materiales.

Mano de obra, tiempo aproximado de obra, días de validez y observaciones. 


Selector de ítems con filtros por nombre, marca y fabricante, con recalculo automático de:

Subtotales.

Total de ítems.

Total final (mano de obra + ítems). 


Botón de “+ Nuevo Ítem” dentro del presupuesto, para no abandonar el flujo de carga. 


Generación de PDF de presupuesto con logo, datos del cliente, tabla de ítems y totales.

Estados de presupuesto (pendiente/aceptado) con indicadores visuales.

Papelera de presupuestos y validación para impedir eliminar presupuestos vinculados a obras. 


4. Gestión de Obras

Alta de obra a partir de un presupuesto y cliente seleccionados.

El sistema muestra solo los presupuestos del cliente, ordenados del más nuevo al más viejo, junto con rubro, total y fecha. 

Campos de obra:

Estado (Iniciada, En Proceso, Finalizada).

Fecha de inicio.

Seguimiento de materiales: quién compra y si ya fueron entregados.

Comentarios/Notas de obra. 


Edición del estado y comentarios de la obra en cualquier momento.

Integración con el calendario visual de Inicio, donde se visualizan las obras activas por día, con accesos rápidos a:

Presupuesto.

Datos del cliente.

Materiales.

Comentarios/actualizaciones. 


5. Visitas Técnicas

ABM de visitas técnicas (mantenimiento, reparación, relevamiento, instalación). 

Selección de cliente y fecha/hora con picker.

Datos: dirección, equipo a revisar, tipo de visita, costo estimado, observaciones/diagnóstico. 


Estados editables: Pendiente, Completada, Cancelada, Reprogramada.

Filtros por cliente y tipo/estado de visita.


6. Papelera (Recuperación de Datos)

Módulo transversal de recuperación de registros:

Clientes

Ítems

Presupuestos

Cada entidad tiene:

Baja lógica (va a papelera).

Restauración desde papelera.

Eliminación física definitiva. 


7. Reportes y estadísticas

Listados y resúmenes para:

Presupuestos aceptados, rechazados o pendientes.

Obras y su estado.

Enfoque en resúmenes claros más que en gráficos complejos, alineado a lo solicitado por el usuario real. 



🏗️ Arquitectura y diseño

El sistema está documentado y desarrollado siguiendo principios de CLEAN Architecture, separación de capas y buenas prácticas de desarrollo. 



Capas principales
Proyecto	Rol
Zetta.BD	Entidades, contexto de EF Core, repositorios (genérico + específicos).
Zetta.Server	API REST .NET 8, controladores, validaciones, automapeo entre entidades y DTOs.
Zetta.Shared	DTOs y contratos compartidos entre cliente y servidor.
Zetta.Client	Frontend en Blazor WebAssembly (páginas, componentes, servicios Http).
Patrones y prácticas

Patrón Repositorio y repositorio genérico para acceso a datos. 



DTOs y AutoMapper para desacoplar modelo de dominio y transporte.

Validaciones con Data Annotations y lógica adicional a nivel de servicio/controlador.

Organización del código orientada a la mantenibilidad y extensibilidad (posible evolución a multiusuario, autenticación, etc.). 



🧪 Flujo funcional resumido
1. Desde cero a una obra en ejecución

Cargar cliente.

Cargar ítems de catálogo.

Crear presupuesto con ítems, mano de obra, tiempo y validez.

Generar PDF y compartir con el cliente.

Una vez aceptado, crear obra vinculada al presupuesto.

Seguir la obra desde el módulo de Obras y el calendario de Inicio. 



2. Seguimiento post-obra

Agendar visitas técnicas (mantenimiento, reparación, etc.).

Registrar diagnóstico, costo estimado y estado de la visita.

Consultar historial de cliente: presupuestos, obras y visitas asociadas. 



El proyecto cuenta con dos documentos formales:
(El mismo puede ser solicitado a los autores del proyecto vía mail: 
leo8292014@gmail.com => Leonardo Contreras
andresnicolaszanetta@gmail.com => Andres Zanetta)

Manual de Desarrollo

Arquitectura general.

Decisiones técnicas, patrones usados, estructura por capas.

Entrevistas con el usuario y análisis de requerimientos. 



Manual de Usuario

Paso a paso con capturas para:

Carga y edición de clientes.

Gestión de ítems, presupuestos, obras y visitas.

Uso de la papelera.

Filtros en todas las pantallas. 


