
using ECN.Contracts.Services;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SimpleWPFReporting;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace ECN.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private Ecn _ecn;

        public Ecn Ecn
        {
            get => _ecn;
            set
            {
                if (_ecn != value)
                {
                    _ecn = value;
                    RaisePropertyChanged("Ecn");
                }
            }
        }

        private ICommand _ExportPDFCommand;
        public ICommand ExportPDFCommand
        {
            get
            {
                if (_ExportPDFCommand == null)
                {
                    _ExportPDFCommand = new RelayCommand<Visual>(ExportECN);
                }
                return _ExportPDFCommand;
            }
        }

        private ObservableCollection<Numberpart> _NumberParts;
        public ObservableCollection<Numberpart> NumberParts
        {
            get => _NumberParts;
            set
            {
                if (_NumberParts != value)
                {
                    _NumberParts = value;
                    RaisePropertyChanged("NumberParts");
                }
            }
        }

        private ObservableCollection<Attachment> _Attachments;
        public ObservableCollection<Attachment> Attachments
        {
            get => _Attachments;
            set
            {
                if (_Attachments != value)
                {
                    _Attachments = value;
                    RaisePropertyChanged("Attachments");
                }
            }
        }

        private ObservableCollection<EcnRevision> _Revisions;
        public ObservableCollection<EcnRevision> Revisions
        {
            get => _Revisions;
            set
            {
                if (_Revisions != value)
                {
                    _Revisions = value;
                    RaisePropertyChanged("Revisions");
                }
            }
        }

        private ObservableCollection<EcnDocumenttype> _Documents;
        public ObservableCollection<EcnDocumenttype> Documents
        {
            get => _Documents;
            set
            {
                if (_Documents != value)
                {
                    _Documents = value;
                    RaisePropertyChanged("Documents");
                }
            }
        }

        public ReportViewModel(Ecn ecn)
        {
            Ecn = ecn;

            Attachments = new ObservableCollection<Attachment>();

            foreach (var item in Ecn.EcnAttachments)
            {
                Attachments.Add(item.Attachment);
            }

            Revisions = new ObservableCollection<EcnRevision>();

            foreach (var item in Ecn.EcnRevisions)
            {
                Revisions.Add(item);
            }
        }

        private void ExportECN(Visual visual)
        {
            Report.ExportVisualAsPdf(visual);
        }
    }
}
