using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class NumberpartType
    {
        public NumberpartType()
        {
            Numberparts = new HashSet<Numberpart>();
        }

        public int NumberPartTypeId { get; set; }
        public string NumberPartTypeName { get; set; }

        public virtual ICollection<Numberpart> Numberparts { get; set; }
    }
}
