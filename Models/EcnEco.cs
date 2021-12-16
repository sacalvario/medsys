using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class EcnEco
    {
        public int IdEcn { get; set; }
        public string IdEco { get; set; }
        public int EcoTypeId { get; set; }

        public virtual EcoType EcoType { get; set; }
        public virtual Ecn IdEcnNavigation { get; set; }
    }
}
