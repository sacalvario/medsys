using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class PruebasLaboratorioResultado
    {
        public PruebasLaboratorioResultado()
        {
            DiagnosticosLabpruebas = new HashSet<DiagnosticosLabprueba>();
        }

        public int IdPrueba { get; set; }
        public int TipoPrueba { get; set; }
        public DateTime FechaPuebra { get; set; }
        public string DescripcionPrueba { get; set; }
        public string Resultado { get; set; }

        public virtual LaboratorioPrueba TipoPruebaNavigation { get; set; }
        public virtual ICollection<DiagnosticosLabprueba> DiagnosticosLabpruebas { get; set; }
    }
}
