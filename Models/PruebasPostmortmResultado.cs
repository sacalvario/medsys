using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class PruebasPostmortmResultado
    {
        public PruebasPostmortmResultado()
        {
            DiagnosticosPostpruebas = new HashSet<DiagnosticosPostprueba>();
        }

        public int IdResultado { get; set; }
        public int IdPrueba { get; set; }
        public string FechaPrueba { get; set; }
        public string DescripcionPrueba { get; set; }
        public string Resultado { get; set; }

        public virtual PostmortemPrueba IdPruebaNavigation { get; set; }
        public virtual ICollection<DiagnosticosPostprueba> DiagnosticosPostpruebas { get; set; }
    }
}
