using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class PostmortemPrueba
    {
        public PostmortemPrueba()
        {
            PruebasPostmortmResultados = new HashSet<PruebasPostmortmResultado>();
        }

        public int IdPruebaPost { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<PruebasPostmortmResultado> PruebasPostmortmResultados { get; set; }
    }
}
