using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class LaboratorioPrueba
    {
        public LaboratorioPrueba()
        {
            PruebasLaboratorioResultados = new HashSet<PruebasLaboratorioResultado>();
        }

        public int IdPruebaLab { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<PruebasLaboratorioResultado> PruebasLaboratorioResultados { get; set; }
    }
}
