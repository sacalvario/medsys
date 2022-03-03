

#nullable disable

namespace ECN.Models
{
    public partial class EcnNumberpart
    {
        public int EcnId { get; set; }
        public int ProductId { get; set; }

        public virtual Ecn Ecn { get; set; }
        public virtual Numberpart Product { get; set; }
    }
}
