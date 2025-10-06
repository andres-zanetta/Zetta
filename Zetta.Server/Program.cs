using Microsoft.EntityFrameworkCore;
using SERVER.Repositorio;
using System.Text.Json.Serialization;
using Zetta.BD.DATA;
using Zetta.BD.DATA.REPOSITORY;
using Zetta.Server.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conn"));

builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped<IPresupuestoRepositorio, PresupuestoRepositorio>();
builder.Services.AddScoped<IItemPresupuestoRepositorio, ItemPresupuestoRepositorio>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IObraRepositorio, ObraRepositorio>();
builder.Services.AddScoped<IItemPresupuestoRepositorio, ItemPresupuestoRepositorio>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
