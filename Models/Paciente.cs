using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            Cita = new HashSet<Cita>();
        }

        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }

        public virtual ICollection<Cita> Cita { get; set; }
    }
}
