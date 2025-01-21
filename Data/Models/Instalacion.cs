using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Instalacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; } // "Deportiva", "Recreativa", etc.
        public int Capacidad { get; set; }
        public string Descripcion { get; set; }
        public string Dia { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Disponibilidad { get; set; } // "Disponible", "Reservada", etc.
        [JsonIgnore]
        public virtual ICollection<ReservacionInstalacion> ReservacionInstalaciones { get; set; }
    }
}
