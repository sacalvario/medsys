using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Enfermedad
    {
        public Enfermedad()
        {
            Diagnosticos = new HashSet<Diagnostico>();
        }

        public int IdEnfermedad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Diagnostico> Diagnosticos { get; set; }
    }
}
