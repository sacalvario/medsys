using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Cita
    {
        public int IdCita { get; set; }
        public int IdUsuario { get; set; }
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEstado { get; set; }
        public TimeSpan Hora { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual Usuario IdMedicoNavigation { get; set; }
        public virtual Paciente IdPacienteNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual CitasDiagnostico CitasDiagnostico { get; set; }
    }
}
