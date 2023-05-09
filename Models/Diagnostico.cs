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
            DiagnosticoSignos = new HashSet<DiagnosticoSigno>();
            DiagnosticoSintomas = new HashSet<DiagnosticoSintoma>();
            DiagnosticosLabpruebas = new HashSet<DiagnosticosLabprueba>();
            DiagnosticosTratamientos = new HashSet<DiagnosticosTratamiento>();
        }

        public int IdDiagnostico { get; set; }
        public int IdEnfermedad { get; set; }

        public virtual Enfermedad IdEnfermedadNavigation { get; set; }
        public virtual ICollection<CitasDiagnostico> CitasDiagnosticos { get; set; }
        public virtual ICollection<DiagnosticoSigno> DiagnosticoSignos { get; set; }
        public virtual ICollection<DiagnosticoSintoma> DiagnosticoSintomas { get; set; }
        public virtual ICollection<DiagnosticosLabprueba> DiagnosticosLabpruebas { get; set; }
        public virtual ICollection<DiagnosticosTratamiento> DiagnosticosTratamientos { get; set; }
    }
}
