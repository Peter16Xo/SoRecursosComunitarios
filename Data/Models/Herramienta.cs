﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Herramienta
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public string Disponibilidad { get; set; } // "Disponible", "Reservada", etc. o algo mas
        [JsonIgnore]
        public virtual ICollection<ReservacionHerramienta> ReservacionHerramientas { get; set; } = new List<ReservacionHerramienta>();
    }

}
