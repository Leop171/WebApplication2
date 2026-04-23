using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PreguntaDTO
    {
        public int Codigo { get; set; }
        public int CodigoEncuesta { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public bool Requerido { get; set; }
        public string tipo { get; set; }


    }
}