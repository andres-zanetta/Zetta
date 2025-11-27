using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.BD.DATA.ENTITY
{
    public class Comentario:EntityBase
    {
        public string Texto { get; set; } // Contenido del comentario

        public DateTime Fecha { get; set; } // Fecha y hora del comentario
        public int ObraId { get; set; }
        public Obra Obra { get; set; } // Obra a la que pertenece el comentario
    }
}
