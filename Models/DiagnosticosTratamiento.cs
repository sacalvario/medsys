using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class DiagnosticosTratamiento
    {
        public int IdDiagnostico { get; set; }
        public int IdTratamiento { get; set; }

        public virtual Diagnostico IdDiagnosticoNavigation { get; set; }
        public virtual Tratamiento IdTratamientoNavigation { get; set; }
    }
}
