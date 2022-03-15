using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class EcnRevision : ViewModelBase
    {
        public int EcnId { get; set; }
        public int RevisionSequence { get; set; }
        public int EmployeeId { get; set; }
        public int StatusId { get; set; }
        public string Notes { get; set; }
        public DateTime? RevisionDate { get; set; }
        public string LongRevisionDate => RevisionDate.HasValue ? RevisionDate.Value.ToLongDateString() : string.Empty;
        public string LongRevisionHour => RevisionDate.HasValue ? RevisionDate.Value.ToLongTimeString() : string.Empty;
        public virtual Ecn Ecn { get; set; }
        public virtual Employee Employee { get; set; }

        private Status _Status;
        public virtual Status Status
        {
            get => _Status;
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }
    }
}
