using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Models
{
    public class ReservacionHerramienta
    {
        public int Id { get; set; }
        public int Usuario_ID { get; set; }
        public int Herramienta_ID { get; set; }
        public string Dia { get; set; } 
        public DateTime Fecha { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Disponibilidad { get; set; } // "Reservada", "Finalizada"
        public virtual Herramienta Herramienta { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

}
