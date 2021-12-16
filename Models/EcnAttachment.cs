using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class EcnAttachment
    {
        public int EcnId { get; set; }
        public int AttachmentId { get; set; }

        public virtual Attachment Attachment { get; set; }
        public virtual Ecn Ecn { get; set; }
    }
}
