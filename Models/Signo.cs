using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Signo
    {
        public Signo()
        {
            DiagnosticoSignos = new HashSet<DiagnosticoSigno>();
        }

        public int IdSigno { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<DiagnosticoSigno> DiagnosticoSignos { get; set; }
    }
}
