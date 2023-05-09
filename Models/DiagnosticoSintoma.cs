using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class DiagnosticoSintoma
    {
        public int IdDiagnostico { get; set; }
        public int IdSintoma { get; set; }

        public virtual Diagnostico IdDiagnosticoNavigation { get; set; }
        public virtual Sintoma IdSintomaNavigation { get; set; }
    }
}
