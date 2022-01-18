using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class EcnDocumenttype
    {
        public int EcnId { get; set; }
        public int DocumentTypeId { get; set; }

        public virtual Documenttype DocumentType { get; set; }
        public virtual Ecn Ecn { get; set; }
    }
}
