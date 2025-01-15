using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Reportes
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string RecursoAfectado { get; set; } // Agua, Electricidad, etc.
        public bool Estado { get; set; } // True = Solucionado, False = Sin solucionar
    }
}
