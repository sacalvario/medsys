using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows;

#nullable disable

namespace ECN.Models
{
    public partial class Ecn : ViewModelBase
    {
        public Ecn()
        {
            EcnAttachments = new HashSet<EcnAttachment>();
            EcnDocumenttypes = new HashSet<EcnDocumenttype>();
            EcnNumberparts = new HashSet<EcnNumberpart>();
            EcnRevisions = new HashSet<EcnRevision>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get => _EndDate;
            set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    RaisePropertyChanged("EndDate");
                }
            }
        }
        public int ChangeTypeId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentName { get; set; }
        public string DocumentLvl { get; set; }
        public string DrawingLvl { get; set; }
        public string OldDrawingLvl { get; set; }
        public string OldDocumentLvl { get; set; }
        public int EmployeeId { get; set; }
        public sbyte IsEco { get; set; }
        public string ChangeDescription { get; set; }
        public string ChangeJustification { get; set; }
        public string ManufacturingAffectations { get; set; }
        public int StatusId { get; set; }
        public int Year => StartDate.Year;
        public int Month => StartDate.Month;
        public string MonthName => System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(StartDate.Month);
        public int Day => StartDate.Day;
        public string ShortDate => StartDate.ToShortDateString();
        public string LongDate => StartDate.ToLongDateString();
        public string LongEndDate => EndDate.ToLongDateString();

        private Visibility _IsEcoVisibility = Visibility.Collapsed;
        public Visibility IsEcoVisibility
        {
            get
            {
                if (Convert.ToBoolean(IsEco))
                {
                    _IsEcoVisibility = Visibility.Visible;
                }
                else
                {
                    _IsEcoVisibility = Visibility.Collapsed;
                }

                return _IsEcoVisibility;
            }
        }


        public bool Is_Eco => Convert.ToBoolean(IsEco);
        public string IsEcoToString => Convert.ToBoolean(IsEco) ? "Sí" : "No";


        public virtual Changetype ChangeType { get; set; }
        public virtual Documenttype DocumentType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Status Status { get; set; }
        public virtual EcnEco EcnEco { get; set; }
        public virtual ICollection<EcnAttachment> EcnAttachments { get; set; }
        public virtual ICollection<EcnDocumenttype> EcnDocumenttypes { get; set; }
        public virtual ICollection<EcnNumberpart> EcnNumberparts { get; set; }
        public virtual ICollection<EcnRevision> EcnRevisions { get; set; }
    }
}
