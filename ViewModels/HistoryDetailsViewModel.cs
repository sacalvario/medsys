using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace ECN.ViewModels
{
    public class HistoryDetailsViewModel : ViewModelBase, INavigationAware
    {
        private IEcnDataService _ecnDataService;
        private INumberPartsDataService _numberPartsDataService;
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

        private Visibility _EcnRegisterTypeVisibility = Visibility.Collapsed;
        public Visibility EcnRegisterTypeVisibility
        {
            get => _EcnRegisterTypeVisibility;
            set
            {
                if (_EcnRegisterTypeVisibility != value)
                {
                    _EcnRegisterTypeVisibility = value;
                    RaisePropertyChanged("EcnRegisterTypeVisibility");
                }
            }
        }

        private Visibility _EcnIntExtTypeVisibility = Visibility.Visible;
        public Visibility EcnIntExtTypeVisibility
        {
            get => _EcnIntExtTypeVisibility;
            set
            {
                if (_EcnIntExtTypeVisibility != value)
                {
                    _EcnIntExtTypeVisibility = value;
                    RaisePropertyChanged("EcnIntExtTypeVisibility");
                }
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

        private Attachment _SelectedAttachment;
        public Attachment SelectedAttachment
        {
            get => _SelectedAttachment;
            set
            {
                if (_SelectedAttachment != value)
                {
                    _SelectedAttachment = value;
                    RaisePropertyChanged();
                }
            }
        }

        private RelayCommand _DownloadAttachmentCommand;
        public RelayCommand DownloadAttachmentCommand
        {
            get
            {
                if (_DownloadAttachmentCommand == null)
                {
                    _DownloadAttachmentCommand = new RelayCommand(DownloadAttachment);
                }
                return _DownloadAttachmentCommand;
            }
        }

        

        public HistoryDetailsViewModel(IEcnDataService ecnDataService, INumberPartsDataService numberPartsDataService)
        {
            Ecn = new Ecn();
            _ecnDataService = ecnDataService;
            _numberPartsDataService = numberPartsDataService;

        }

        public void OnNavigatedFrom()
        {

        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Ecn ecn)
            {
                Ecn = ecn;
            }

            if (Ecn.ChangeType.ChangeTypeId == 3)
            {
                EcnRegisterTypeVisibility = Visibility.Visible;
                EcnIntExtTypeVisibility = Visibility.Collapsed;
            }
            else
            {
                if (EcnIntExtTypeVisibility == Visibility.Collapsed)
                {
                    EcnIntExtTypeVisibility = Visibility.Visible;
                    EcnRegisterTypeVisibility = Visibility.Collapsed;
                }
            }

            NumberParts = new ObservableCollection<Numberpart>();
            Attachments = new ObservableCollection<Attachment>();
            Revisions = new ObservableCollection<EcnRevision>();
            Documents = new ObservableCollection<EcnDocumenttype>();
            GetNumberParts();
            GetAttachments();
            GetRevisions();
            GetDocuments();

        }

        private async void GetNumberParts()
        {
            Ecn.EcnNumberparts = await _numberPartsDataService.GetNumberPartsEcnsAsync(Ecn.Id);

            foreach(var item in Ecn.EcnNumberparts)
            {
                var np = await _numberPartsDataService.GetNumberPartAsync(item.ProductId);
                np.NumberPartTypeNavigation = await _numberPartsDataService.GetNumberpartTypeAsync(np.NumberPartType);
                np.Customer = await _numberPartsDataService.GetCustomerAsync(np.CustomerId);
                NumberParts.Add(np);
            }

        }

        private async void GetAttachments()
        {
            Ecn.EcnAttachments = await _ecnDataService.GetAttachmentsAsync(Ecn.Id);

            foreach (var item in Ecn.EcnAttachments)
            {
                var attached = await _ecnDataService.GetAttachmentAsync(item.AttachmentId);
                attached.Extension = Path.GetExtension(attached.AttachmentFilename);
                Attachments.Add(attached);
            }

        }

        private async void GetRevisions()
        {
            Ecn.EcnRevisions = await _ecnDataService.GetRevisionsAsync(Ecn.Id);

            foreach (var item in Ecn.EcnRevisions)
            {
                item.Employee = await _ecnDataService.GetEmployeeAsync(item.EmployeeId);
                item.Employee.Department = await _ecnDataService.GetDepartmentAsync(item.Employee.DepartmentId);
                item.Status = await _ecnDataService.GetStatusAsync(item.StatusId);
                Revisions.Add(item);
            }
        }

        private async void GetDocuments()
        {
            Ecn.EcnDocumenttypes = await _ecnDataService.GetDocumentsAsync(Ecn.Id);

            foreach (var item in Ecn.EcnDocumenttypes)
            {
                item.DocumentType = await _ecnDataService.GetDocumentTypeAsync(item.DocumentTypeId);
                Documents.Add(item);
            }

        }

        private void DownloadAttachment()
        {
            File.WriteAllBytes(@"C:\Development\Adjuntos\" + SelectedAttachment.AttachmentFilename, SelectedAttachment.AttachmentFile);
        }
    }
}
