
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
        public Attachment(string ext)
        {
            EcnAttachments = new HashSet<EcnAttachment>();
            ImageLocation = SetImage(ext);
        }

        public int AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentFilename { get; set; }
        public byte[] AttachmentFile { get; set; }

        private string _Extension;
        public string Extension 
        {
            get => _Extension;
            set
            {
                if (_Extension != value)
                {
                    _Extension = value;
                    ImageLocation = SetImage(_Extension);
                }
            }
        }
        public string ImageLocation { get; set; }

        private string SetImage(string ext)
        {
            if (ext == ".xlsx" || ext == ".xls")
            {
                return $"/Assets/excel2.png";
            }
            else if (ext == ".pdf")
            {
                return $"/Assets/pdf.png";
            }
            else if (ext == ".doc" || ext == ".docx")
            {
                return $"/Assets/word.png";
            }
            else if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".gif")
            {
                return $"/Assets/image.png";
            }
            else if (ext == ".ppt" || ext == ".pptx")
            {
                return $"/Assets/powerpoint.png";
            }
            return $"/Assets/other.png";
        }

        public virtual ICollection<EcnAttachment> EcnAttachments { get; set; }
    }
}
