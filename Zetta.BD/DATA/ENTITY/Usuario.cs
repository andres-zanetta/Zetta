using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.BD.DATA.ENTITY
{
    public class Usuario: EntityBase
    {
        public int DNI { get; set; } //clave
        public string NombreU { get; set; }//nombre usuario
    }
}
