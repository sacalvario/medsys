using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Documenttype
    {
        public Documenttype()
        {
            Ecns = new HashSet<Ecn>();
        }

        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }

        public virtual ICollection<Ecn> Ecns { get; set; }
    }
}
