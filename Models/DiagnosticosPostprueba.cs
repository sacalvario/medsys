using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class DiagnosticosPostprueba
    {
        public int IdDiagnostico { get; set; }
        public int IdPrueba { get; set; }

        public virtual PruebasPostmortmResultado IdPruebaNavigation { get; set; }
    }
}
