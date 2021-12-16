using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class EcoType
    {
        public EcoType()
        {
            EcnEcos = new HashSet<EcnEco>();
        }

        public int EcoTypeId { get; set; }
        public string EcoTypeName { get; set; }

        public virtual ICollection<EcnEco> EcnEcos { get; set; }
    }
}
