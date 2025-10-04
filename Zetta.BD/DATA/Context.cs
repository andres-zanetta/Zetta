using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zetta.BD.DATA.ENTITY;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Zetta.BD.DATA
{
    public class Context : DbContext
    {
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<ItemPresupuesto> ItemPresupuestos { get; set; }
        public DbSet<PresItemDetalle> PresItemDetalles { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Obra> Obras { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Configuración de Presupuesto
            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.Property(p => p.Total)
                      .HasPrecision(18, 2);

                entity.HasOne(p => p.Cliente)
                      .WithMany(c => c.Presupuestos)
                      .HasForeignKey(p => p.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.ItemsDetalle)
                      .WithOne(i => i.Presupuesto)
                      .HasForeignKey(i => i.PresupuestoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 2. Configuración de Obra
            modelBuilder.Entity<Obra>(entity =>
            {
                entity.HasOne(o => o.Cliente)
                      .WithMany(c => c.Obras)
                      .HasForeignKey(o => o.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.Presupuesto)
                      .WithOne() // Relación 1:1 (una obra tiene un presupuesto)
                      .HasForeignKey<Obra>(o => o.PresupuestoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 3. Configuración de ItemPresupuesto y Detalles
            modelBuilder.Entity<ItemPresupuesto>(entity =>
            {
                entity.Property(i => i.Precio)
                      .HasPrecision(18, 2);
            });

            modelBuilder.Entity<PresItemDetalle>(entity =>
            {
                entity.Property(d => d.PrecioUnitario)
                      .HasPrecision(18, 2);

                entity.HasOne(d => d.ItemPresupuesto)
                      .WithMany()
                      .HasForeignKey(d => d.ItemPresupuestoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}

//| Bloque | Función | Motivo |
//| ----------------------------------- | ------------------------------------------------------ | --------------------------------------- |
//| `cascadeFKs`                        | Cambia *DeleteBehavior.Cascade → Restrict* globalmente | Evita eliminaciones no deseadas         |
//| `HasPrecision(18,2)`                | Define precisión decimal en SQL Server                 | Previene redondeos o errores monetarios |
//| `.HasOne().WithMany()`              | Define relaciones explícitas entre entidades           | Mejora control sobre foreign keys       |
//| `OnDelete(DeleteBehavior.Restrict)` | Impide borrar registros relacionados sin control       | Recomendado en sistemas de gestión      |

