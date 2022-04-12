
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Numberpart
    {
        public Numberpart()
        {
            EcnNumberparts = new HashSet<EcnNumberpart>();
        }

        public int NumberPartNo { get; set; }
        public string NumberPartId { get; set; }
        public int CustomerId { get; set; }
        public string NumberPartRev { get; set; }
        public int NumberPartType { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual NumberpartType NumberPartTypeNavigation { get; set; }
        public virtual ICollection<EcnNumberpart> EcnNumberparts { get; set; }
    }
}
