using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Diagnostico
    {
        public Diagnostico()
        {
            CitasDiagnosticos = new HashSet<CitasDiagnostico>();
            DiagnosticoSintomas = new HashSet<DiagnosticoSintoma>();
            DiagnosticosTratamientos = new HashSet<DiagnosticosTratamiento>();
        }

        public int IdDiagnostico { get; set; }
        public int IdEnfermedad { get; set; }
        public int? IdPruebaPostMortem { get; set; }
        public int? IdPruebaLab { get; set; }
        public sbyte TienePruebaPost { get; set; }
        public sbyte TienePruebaLab { get; set; }

        public virtual Enfermedad IdEnfermedadNavigation { get; set; }
        public virtual PruebasLaboratorioResultado IdPruebaLabNavigation { get; set; }
        public virtual PruebasPostmortmResultado IdPruebaPostMortemNavigation { get; set; }
        public virtual ICollection<CitasDiagnostico> CitasDiagnosticos { get; set; }
        public virtual ICollection<DiagnosticoSintoma> DiagnosticoSintomas { get; set; }
        public virtual ICollection<DiagnosticosTratamiento> DiagnosticosTratamientos { get; set; }
    }
}
