using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public char Active { get; set; } //Y-N
        [JsonIgnore]
        public virtual ICollection<ReservacionInstalacion> ReservacionInstalaciones { get; set; } = new List<ReservacionInstalacion>();
        [JsonIgnore]
        public virtual ICollection<ReservacionHerramienta> ReservacionHerramientas { get; set; } = new List<ReservacionHerramienta>();
    }
}
