using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class EncuestaTMP
    {
        public List<PreguntaTMP> Preguntas { get; set; }
    }
    public class PreguntaTMP
    {
        public int Id { get; set; }
        public string Respuesta { get; set; }
    }

}
