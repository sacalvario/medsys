using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Status
    {
        public Status()
        {
            EcnRevisions = new HashSet<EcnRevision>();
            Ecns = new HashSet<Ecn>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<EcnRevision> EcnRevisions { get; set; }
        public virtual ICollection<Ecn> Ecns { get; set; }
    }
}
