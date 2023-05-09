using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Cita = new HashSet<Cita>();
        }

        public int IdEstado { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Cita> Cita { get; set; }
    }
}
