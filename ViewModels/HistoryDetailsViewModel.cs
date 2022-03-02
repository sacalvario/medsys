using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class HistoryDetailsViewModel : ViewModelBase, INavigationAware
    {
        private IEcnDataService _ecnDataService;
        private INumberPartsDataService _numberPartsDataService;
        private IOpenFileService _openFileService;
        private IWindowManagerService _windowManagerService;
        private IMailService _mailService;
        private INavigationService _navigationService;
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

        private string _Customer;
        public string Customer
        {
            get => _Customer;
            set
            {
                if (_Customer != value)
                {
                    _Customer = value;
                    RaisePropertyChanged("Customer");
                }
            }
        }

        private string _CustomerRevision;
        public string CustomerRevision
        {
            get => _CustomerRevision;
            set
            {
                if (_CustomerRevision != value)
                {
                    _CustomerRevision = value;
                    RaisePropertyChanged("CustomerRevision");
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

        private Visibility _EcnHistoryTypeVisibility = Visibility.Visible;
        public Visibility EcnHistoryTypeVisibility
        {
            get => _EcnHistoryTypeVisibility;
            set
            {
                if (_EcnHistoryTypeVisibility != value)
                {
                    _EcnHistoryTypeVisibility = value;
                    RaisePropertyChanged("EcnHistoryTypeVisibility");
                }
            }
        }

        private Visibility _EcnSignTypeVisibility = Visibility.Collapsed;
        public Visibility EcnSignTypeVisibility
        {
            get => _EcnSignTypeVisibility;
            set
            {
                if (_EcnSignTypeVisibility != value)
                {
                    _EcnSignTypeVisibility = value;
                    RaisePropertyChanged("EcnSignTypeVisibility");
                }
            }
        }

        private Visibility _EcnNumberPartsVisibility = Visibility.Visible;
        public Visibility EcnNumberPartsVisibility
        {
            get => _EcnNumberPartsVisibility;
            set
            {
                if (_EcnNumberPartsVisibility != value)
                {
                    _EcnNumberPartsVisibility = value;
                    RaisePropertyChanged("EcnNumberPartsVisibility");
                }
            }
        }

        private Visibility _EcnDocumentsVisibility = Visibility.Visible;
        public Visibility EcnDocumentsVisibility
        {
            get => _EcnDocumentsVisibility;
            set
            {
                if (_EcnDocumentsVisibility != value)
                {
                    _EcnDocumentsVisibility = value;
                    RaisePropertyChanged("EcnDocumentsVisibility");
                }
            }
        }

        private Visibility _EcnNumberPartChangeRevision = Visibility.Collapsed;
        public Visibility EcnNumberPartChangeRevision
        {
            get => _EcnNumberPartChangeRevision;
            set
            {
                if (_EcnNumberPartChangeRevision != value)
                {
                    _EcnNumberPartChangeRevision = value;
                    RaisePropertyChanged("EcnNumberPartChangeRevision");
                }
            }
        }

        private Visibility _EcnNumberPartRevision = Visibility.Visible;
        public Visibility EcnNumberPartRevision
        {
            get => _EcnNumberPartRevision;
            set
            {
                if (_EcnNumberPartRevision != value)
                {
                    _EcnNumberPartRevision = value;
                    RaisePropertyChanged("EcnNumberPartRevision");
                }
            }
        }

        private string _Notes;
        public string Notes
        {
            get => _Notes;
            set
            {
                if (_Notes != value)
                {
                    _Notes = value;
                    RaisePropertyChanged("Notes");
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
         
        private int _SelectedTabItem;
        public int SelectedTabItem
        {
            get => _SelectedTabItem;
            set
            {
                if (_SelectedTabItem != value)
                {
                    _SelectedTabItem = value;
                    RaisePropertyChanged("SelectedTabItem");
                }
            }
        }


        private ICommand _DownloadAttachmentCommand;
        public ICommand DownloadAttachmentCommand
        {
            get
            {
                if (_DownloadAttachmentCommand == null)
                {
                    _DownloadAttachmentCommand = new RelayCommand<Attachment>(DownloadAttachment);
                }
                return _DownloadAttachmentCommand;
            }
        }

        private ICommand _VerifiedECNCommand;
        public ICommand VerifiedECNCommand
        {
            get
            {
                if (_VerifiedECNCommand == null)
                {
                    _VerifiedECNCommand = new RelayCommand(VerifiedECN);
                }
                return _VerifiedECNCommand;
            }
        }

        private ICommand _RefuseECNCommand;
        public ICommand RefuseECNCommand
        {
            get
            {
                if (_RefuseECNCommand == null)
                {
                    _RefuseECNCommand = new RelayCommand(RefuseECN);
                }
                return _RefuseECNCommand;
            }
        }

        private ICommand _GoToBackCommand;
        public ICommand GoToBackCommand
        {
            get
            {
                if (_GoToBackCommand == null)
                {
                    _GoToBackCommand = new RelayCommand(GoBack);
                }
                return _GoToBackCommand;
            }
        }

        public HistoryDetailsViewModel(IEcnDataService ecnDataService, INumberPartsDataService numberPartsDataService, IOpenFileService openFileService, IWindowManagerService windowManagerService, IMailService mailService, INavigationService navigationService)
        {
            Ecn = new Ecn();
            _ecnDataService = ecnDataService;
            _numberPartsDataService = numberPartsDataService;
            _openFileService = openFileService;
            _windowManagerService = windowManagerService;
            _mailService = mailService;
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom()
        {
            if (SelectedTabItem == 1)
            {
                SelectedTabItem = 0;
            }

        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Ecn ecn)
            {
                Ecn = ecn;
            }

            if (Ecn.Employee != UserRecord.Employee)
            {
                EcnSignTypeVisibility = Visibility.Visible;
                EcnHistoryTypeVisibility = Visibility.Collapsed;
            }
            else
            {
                if (EcnHistoryTypeVisibility == Visibility.Collapsed)
                {
                    EcnHistoryTypeVisibility = Visibility.Visible;
                    EcnSignTypeVisibility = Visibility.Collapsed;
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

            if (NumberParts.Count != 0)
            {
                Customer = NumberParts[0].Customer.CustomerName;
            }

            if (Ecn.ChangeType.ChangeTypeId == 3)
            {
                EcnRegisterTypeVisibility = Visibility.Visible;
                EcnIntExtTypeVisibility = Visibility.Collapsed;
                EcnDocumentsVisibility = Visibility.Collapsed;
            }
            else
            {
                if (EcnIntExtTypeVisibility == Visibility.Collapsed)
                {
                    EcnIntExtTypeVisibility = Visibility.Visible;
                    EcnDocumentsVisibility = Visibility.Visible;
                    EcnRegisterTypeVisibility = Visibility.Collapsed;
                }
            }

            if (Documents.Count == 0)
            {
                EcnDocumentsVisibility = Visibility.Collapsed;
            }
            else
            {
                if (EcnDocumentsVisibility == Visibility.Collapsed)
                {
                    EcnDocumentsVisibility = Visibility.Visible;
                }
            }

            if (NumberParts.Count == 0)
            {
                EcnNumberPartsVisibility = Visibility.Collapsed;
            }
            else
            {
                if (EcnNumberPartsVisibility == Visibility.Collapsed)
                {
                    EcnNumberPartsVisibility = Visibility.Visible;
                }

                if (Ecn.DocumentType.DocumentTypeId == 2 || Ecn.DocumentType.DocumentTypeId == 4)
                {
                    if (Ecn.ChangeType.ChangeTypeId != 3)
                    {
                        EcnNumberPartChangeRevision = Visibility.Visible;
                        EcnNumberPartRevision = Visibility.Collapsed;
                    }
                    else
                    {
                        if (EcnNumberPartRevision == Visibility.Collapsed)
                        {
                            EcnNumberPartRevision = Visibility.Visible;
                            EcnNumberPartChangeRevision = Visibility.Collapsed;
                        }
                    }
                }
                else
                {
                    if (EcnNumberPartRevision == Visibility.Collapsed)
                    {
                        EcnNumberPartRevision = Visibility.Visible;
                        EcnNumberPartChangeRevision = Visibility.Collapsed;
                    }
                }

                CustomerRevision = NumberParts[0].NumberPartRev;
            }
        }

        private async void GetNumberParts()
        {
            var ecnNumberparts = await _numberPartsDataService.GetNumberPartsEcnsAsync(Ecn.Id);

            foreach(var item in ecnNumberparts)
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

        private void DownloadAttachment(Attachment attachment)
        {
            if (_openFileService.SaveFileDialog(attachment.AttachmentFilename))
            {
                File.WriteAllBytes(_openFileService.Path, attachment.AttachmentFile);

                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo(_openFileService.Path)
                    {
                        UseShellExecute = true
                    }
                };
                _ = process.Start();

            }
        }

        private void VerifiedECN()
        {
            if (_ecnDataService.SignEcn(Ecn, Notes))
            {
                Employee emp = _ecnDataService.NextToSignEcn(Ecn);

                if (emp != null)
                {
                    _mailService.SendSignEmail("scalvario@electri-cord.com.mx", Ecn.Id, emp.Name, Ecn.Employee.Name);
                }

                _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se ha validado el ECN.");
                Notes = string.Empty;
                _navigationService.GoBack();
            }
        }

        private void RefuseECN()
        {
            if (_ecnDataService.RefuseEcn(Ecn, Notes))
            {
                if (Ecn.DocumentType.DocumentTypeId == 3 || Ecn.DocumentType.DocumentTypeId == 9 || Ecn.DocumentType.DocumentTypeId == 14)
                {
                    Employee emp = _ecnDataService.NextToSignEcn(Ecn);

                    if (emp != null)
                    {
                        _mailService.SendSignEmail("scalvario@electri-cord.com.mx", Ecn.Id, emp.Name, Ecn.Employee.Name);
                    }
                    else
                    {
                        foreach (var item in Ecn.EcnRevisions)
                        {
                            _mailService.SendRefuseECNEmail("scalvario@electri-cord.com.mx", Ecn.Id, item.Employee.Name, Ecn.Employee.Name);
                        }

                        _mailService.SendRefuseECNToGeneratorEmail("scalvario@electri-cord.com.mx", Ecn.Id, UserRecord.Employee.Name, Ecn.Employee.Name);
                    }
                }
                else
                {
                    foreach (var item in Ecn.EcnRevisions)
                    {
                        _mailService.SendRefuseECNEmail("scalvario@electri-cord.com.mx", Ecn.Id, item.Employee.Name, Ecn.Employee.Name);
                    }
                    _mailService.SendRefuseECNToGeneratorEmail("scalvario@electri-cord.com.mx", Ecn.Id, UserRecord.Employee.Name, Ecn.Employee.Name);
                }

                _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se ha rechazado el ECN. Se le notificara al generador.");
                Notes = string.Empty;
                _navigationService.GoBack();
            }
        }

        private void GoBack()
        {
            _navigationService.GoBack();
        }
    } 
}
