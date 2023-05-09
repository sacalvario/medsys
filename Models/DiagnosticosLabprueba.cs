using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class DiagnosticosLabprueba
    {
        public int IdDiagnostico { get; set; }
        public int IdPrueba { get; set; }

        public virtual Diagnostico IdDiagnosticoNavigation { get; set; }
        public virtual PruebasLaboratorioResultado IdPruebaNavigation { get; set; }
    }
}
