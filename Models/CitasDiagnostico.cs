using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class CitasDiagnostico
    {
        public int IdCita { get; set; }
        public int IdDiagnostico { get; set; }

        public virtual Cita IdCitaNavigation { get; set; }
        public virtual Diagnostico IdDiagnosticoNavigation { get; set; }
    }
}
