using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class Documenttype : ViewModelBase
    {
        public Documenttype()
        {
            EcnDocumenttypes = new HashSet<EcnDocumenttype>();
            Ecns = new HashSet<Ecn>();
        }

        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }

        private bool _IsSelected = false;
        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        public virtual ICollection<EcnDocumenttype> EcnDocumenttypes { get; set; }
        public virtual ICollection<Ecn> Ecns { get; set; }
    }
}
