using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Sintoma
    {
        public Sintoma()
        {
            DiagnosticoSintomas = new HashSet<DiagnosticoSintoma>();
        }

        public int IdSintoma { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<DiagnosticoSintoma> DiagnosticoSintomas { get; set; }
    }
}
