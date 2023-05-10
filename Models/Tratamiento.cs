using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Tratamiento
    {
        public Tratamiento()
        {
            DiagnosticosTratamientos = new HashSet<DiagnosticosTratamiento>();
        }

        public int IdTratamiento { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<DiagnosticosTratamiento> DiagnosticosTratamientos { get; set; }
    }
}
