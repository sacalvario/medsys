using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class DiagnosticoSigno
    {
        public int IdDiagnostico { get; set; }
        public int IdSigno { get; set; }

        public virtual Diagnostico IdDiagnosticoNavigation { get; set; }
        public virtual Signo IdSignoNavigation { get; set; }
    }
}
