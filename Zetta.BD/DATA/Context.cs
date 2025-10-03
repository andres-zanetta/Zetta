using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zetta.BD.DATA.ENTITY;

namespace Zetta.BD.DATA
{
    public class Context : DbContext
    {
        public DbSet<Presupuesto>Presupuestos { get; set; } 

        public DbSet<ItemPresupuesto> ItemsPresupuesto { get; set; }

        public DbSet<PresItemDetalle> PresItemDetalles { get; set; }


        public Context(DbContextOptions options) : base(options)
        {

        }
        //Definir las entidades
    }
}
