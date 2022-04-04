using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Attachment
    {
        public Attachment()
        {
            EcnAttachments = new HashSet<EcnAttachment>();
        }

        public int AttachmentId { get; set; }
        public string AttachmentFilename { get; set; }
        public byte[] AttachmentFile { get; set; }

        public virtual ICollection<EcnAttachment> EcnAttachments { get; set; }
    }
}
