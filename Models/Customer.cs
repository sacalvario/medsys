using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Numberparts = new HashSet<Numberpart>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public virtual ICollection<Numberpart> Numberparts { get; set; }
    }
}
