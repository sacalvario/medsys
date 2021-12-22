using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class EcnRevision
    {
        public int EcnId { get; set; }
        public int RevisionSequence { get; set; }
        public int EmployeeId { get; set; }
        public int StatusId { get; set; }
        public string Notes { get; set; }
        public DateTime? RevisionDate { get; set; }
        public TimeSpan? RevisionHour { get; set; }
        public string LongRevisionDate => RevisionDate.HasValue ? RevisionDate.Value.ToLongDateString() : null;
        public string LongRevisionHour => RevisionDate.HasValue ? RevisionDate.Value.ToLongTimeString() : null;
        public virtual Ecn Ecn { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Status Status { get; set; }
    }
}
