using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ReservacionInstalacion
    {
        public int ID { get; set; }
        public int Usuario_ID { get; set; }
        public int Instalacion_ID { get; set; }
        public DateOnly? Fecha { get; set; }
        public string Disponibilidad { get; set; } // "Reservada", "Finalizada"
        public virtual Instalacion? Instalacion { get; set; } = null;
        public virtual Usuario? Usuario { get; set; }=null;
    }
}
 