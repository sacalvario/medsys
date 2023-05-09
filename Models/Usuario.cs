using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            CitaIdMedicoNavigations = new HashSet<Cita>();
            CitaIdUsuarioNavigations = new HashSet<Cita>();
        }

        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string TipoUsuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<Cita> CitaIdMedicoNavigations { get; set; }
        public virtual ICollection<Cita> CitaIdUsuarioNavigations { get; set; }
    }
}
