using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Zetta.Client;
using Zetta.Client.Servicios; 

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Servicios
builder.Services.AddScoped<ItemPresupuestoService>();
builder.Services.AddScoped<IItemPresupuestoService, ItemPresupuestoService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<PresupuestoService>();
builder.Services.AddScoped<IPresupuestoServices, PresupuestoService>();
builder.Services.AddScoped<ObraService>();
builder.Services.AddScoped<IObraService, ObraService>();
builder.Services.AddScoped<EstadisticaService>();
builder.Services.AddScoped<IEstadisticaService, EstadisticaService>();
builder.Services.AddScoped<VisitaTecnicaService>();
builder.Services.AddScoped<IVisitaTecnicaService, VisitaTecnicaService>();


await builder.Build().RunAsync();
