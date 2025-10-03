using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.BD.DATA.ENTITY;

namespace Z.BD.DATA
{
    public class Context : DbContext
    {

        public DbSet<Cliente> Clientes { get; set; }


        public Context(DbContextOptions options) : base(options)
        {

        }


    }
}

