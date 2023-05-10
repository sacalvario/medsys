using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Enfermedade
    {
        public Enfermedade()
        {
            Diagnosticos = new HashSet<Diagnostico>();
        }

        public int IdEnfermedad { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Diagnostico> Diagnosticos { get; set; }
    }
}
