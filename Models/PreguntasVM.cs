using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PreguntasVM
    {
        public int Codigo { get; set; }
        public int CodigoEncuesta { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public bool Requerido { get; set; }
        public string Tipo { get; set; }

        // Campo temporal para la vista
        public string Respuesta { get; set; }
    }
}

