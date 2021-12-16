using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Changetype
    {
        public Changetype()
        {
            Ecns = new HashSet<Ecn>();
        }

        public int ChangeTypeId { get; set; }
        public string ChangeTypeName { get; set; }

        public virtual ICollection<Ecn> Ecns { get; set; }
    }
}
