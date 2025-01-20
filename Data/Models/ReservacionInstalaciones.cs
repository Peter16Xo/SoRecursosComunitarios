using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ReservacionInstalaciones
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int InstalacionId { get; set; }
        public DateTime Fecha { get; set; }
        public string Disponibilidad { get; set; } // "Reservada", "Finalizada"
    }
}
 