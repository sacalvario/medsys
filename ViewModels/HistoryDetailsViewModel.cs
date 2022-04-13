
using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class HistoryDetailsViewModel : ViewModelBase, INavigationAware
    {
        public IEcnDataService _ecnDataService;
        public INumberPartsDataService _numberPartsDataService;
        private readonly IOpenFileService _openFileService;
        private readonly IWindowManagerService _windowManagerService;
        private readonly IMailService _mailService;
        private readonly INavigationService _navigationService;

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

        private Visibility _EcnCloseTypeVisibility = Visibility.Collapsed;
        public Visibility EcnCloseTypeVisibility
        {
            get => _EcnCloseTypeVisibility;
            set
            {
                if (_EcnCloseTypeVisibility != value)
                {
                    _EcnCloseTypeVisibility = value;
                    RaisePropertyChanged("EcnCloseTypeVisibility");
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

        private Visibility _EcnPropietaryVisibility = Visibility.Collapsed;
        public Visibility EcnPropietaryVisibility
        {
            get => _EcnPropietaryVisibility;
            set
            {
                if (_EcnPropietaryVisibility != value)
                {
                    _EcnPropietaryVisibility = value;
                    RaisePropertyChanged("EcnPropietaryVisibility");
                }
            }
        }

        private Visibility _EcnCloseDateVisibility = Visibility.Collapsed;
        public Visibility EcnCloseDateVisibility
        {
            get => _EcnCloseDateVisibility;
            set
            {
                if (_EcnCloseDateVisibility != value)
                {
                    _EcnCloseDateVisibility = value;
                    RaisePropertyChanged("EcnCloseDateVisibility");
                }
            }
        }

        private Visibility _EcnEstimateCloseDateVisibility = Visibility.Visible;
        public Visibility EcnEstimateCloseDateVisibility
        {
            get => _EcnEstimateCloseDateVisibility;
            set
            {
                if (_EcnEstimateCloseDateVisibility != value)
                {
                    _EcnEstimateCloseDateVisibility = value;
                    RaisePropertyChanged("EcnEstimateCloseDateVisibility");
                }
            }
        }

        private Visibility _ModifyAttachmentVisibility = Visibility.Collapsed;
        public Visibility ModifyAttachmentVisibility
        {
            get => _ModifyAttachmentVisibility;
            set
            {
                if (_ModifyAttachmentVisibility != value)
                {
                    _ModifyAttachmentVisibility = value;
                    RaisePropertyChanged("ModifyAttachmentVisibility");
                }
            }
        }

        private Visibility _UpgradeECNVisibility = Visibility.Collapsed;
        public Visibility UpgradeECNVisibility
        {
            get => _UpgradeECNVisibility;
            set
            {
                if (_UpgradeECNVisibility != value)
                {
                    _UpgradeECNVisibility = value;
                    RaisePropertyChanged("UpgradeECNVisibility");
                }
            }
        }

        private Visibility _ECNNotesVisibility = Visibility.Collapsed;
        public Visibility ECNNotesVisibility
        {
            get => _ECNNotesVisibility;
            set
            {
                if (_ECNNotesVisibility != value)
                {
                    _ECNNotesVisibility = value;
                    RaisePropertyChanged("ECNNotesVisibility");
                }
            }
        }


        private string _RevisionNotes;
        public string RevisionNotes
        {
            get => _RevisionNotes;
            set
            {
                if (_RevisionNotes != value)
                {
                    _RevisionNotes = value;
                    RaisePropertyChanged("RevisionNotes");
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

        private ICommand _ExportPDFCommand;
        public ICommand ExportPDFCommand
        {
            get
            {
                if (_ExportPDFCommand == null)
                {
                    _ExportPDFCommand = new RelayCommand(ExportECN);
                }
                return _ExportPDFCommand;
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

        private ICommand _CloseECNCommand;
        public ICommand CloseECNCommand
        {
            get
            {
                if (_CloseECNCommand == null)
                {
                    _CloseECNCommand = new RelayCommand(CloseECN);
                }
                return _CloseECNCommand;
            }
        }

        private ICommand _CancelECNCommand;
        public ICommand CancelECNCommand
        {
            get
            {
                if (_CancelECNCommand == null)
                {
                    _CancelECNCommand = new RelayCommand(CancelECN);
                }
                return _CancelECNCommand;
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

        private ICommand _ChangeAttachmentCommand;
        public ICommand ChangeAttachmentCommand
        {
            get
            {
                if (_ChangeAttachmentCommand == null)
                {
                    _ChangeAttachmentCommand = new RelayCommand<Attachment>(ChangeAttachment);
                }
                return _ChangeAttachmentCommand;
            }
        }

        private ICommand _UpgradeECNCommand;
        public ICommand UpgradeECNCommand
        {
            get
            {
                if (_UpgradeECNCommand == null)
                {
                    _UpgradeECNCommand = new RelayCommand(UpgradeECN);
                }
                return _UpgradeECNCommand;
            }
        }

        public HistoryDetailsViewModel(IEcnDataService ecnDataService, INumberPartsDataService numberPartsDataService, IOpenFileService openFileService, IWindowManagerService windowManagerService, IMailService mailService, INavigationService navigationService)
        {
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

            if (EcnPropietaryVisibility == Visibility.Visible)
            {
                EcnPropietaryVisibility = Visibility.Collapsed;
            }

            if (EcnSignTypeVisibility == Visibility.Visible)
            {
                EcnSignTypeVisibility = Visibility.Collapsed;
            }

            if (EcnCloseTypeVisibility == Visibility.Visible)
            {
                EcnCloseTypeVisibility = Visibility.Collapsed;
            }

            if (ModifyAttachmentVisibility == Visibility.Visible)
            {
                ModifyAttachmentVisibility = Visibility.Collapsed;
            }

            if (UpgradeECNVisibility == Visibility.Visible)
            {
                UpgradeECNVisibility = Visibility.Collapsed;
            }

            if (ECNNotesVisibility == Visibility.Visible)
            {
                ECNNotesVisibility = Visibility.Collapsed;
            }
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Ecn ecn)
            {
                Ecn = new Ecn();
                Ecn = ecn;
            }

            if (Ecn.Status.StatusId == 3)
            {
                EcnCloseDateVisibility = Visibility.Visible;
                EcnEstimateCloseDateVisibility = Visibility.Collapsed;
            }
            else if (Ecn.Status.StatusId == 1 && Ecn.EmployeeId == UserRecord.Employee_ID)
            {
                ModifyAttachmentVisibility = Visibility.Visible;
            }
            else
            {
                if (EcnEstimateCloseDateVisibility == Visibility.Collapsed)
                {
                    EcnEstimateCloseDateVisibility = Visibility.Visible;
                    EcnCloseDateVisibility = Visibility.Collapsed;
                }
            }

            if ((Ecn.Status.StatusId == 3 || Ecn.Status.StatusId == 2) && Ecn.Notes != string.Empty && Ecn.Notes != null)
            {
                ECNNotesVisibility = Visibility.Visible;
            }

            if (Ecn.EmployeeId != UserRecord.Employee_ID)
            {
                EcnPropietaryVisibility = Visibility.Visible;
            }

            if (Ecn.CurrentSignature != null)
            {
                EcnSignTypeVisibility = Visibility.Visible;
                EcnCloseTypeVisibility = Visibility.Collapsed;
            }

            else if (UserRecord.Employee_ID == 3806 && Ecn.Status.StatusId == 4)
            {
                if (EcnCloseTypeVisibility == Visibility.Collapsed)
                {
                    EcnCloseTypeVisibility = Visibility.Visible;
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

            Attachments.CollectionChanged += AttachmentsCollectionChanged;

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

                if (Ecn.DocumentType.DocumentTypeId == 2 || Ecn.DocumentType.DocumentTypeId == 4 || Ecn.DocumentType.DocumentTypeId == 15 || Ecn.DocumentType.DocumentTypeId == 16 || Ecn.DocumentType.DocumentTypeId == 17)
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
        private void AttachmentsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                if (_UpgradeECNVisibility != Visibility.Visible)
                {
                    UpgradeECNVisibility = Visibility.Visible;
                }
            }
        }

        private async void GetNumberParts()
        {
            var numberparts = await _numberPartsDataService.GetNumberPartsEcnsAsync(Ecn.Id);

            foreach(var item in numberparts)
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
                Attachments.Add(attached);
            }

        }

        private async void GetRevisions()
        {
            var revisions = await _ecnDataService.GetRevisionsAsync(Ecn.Id);

            foreach (var item in revisions)
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

        private void ChangeAttachment(Attachment attachment)
        {
            if (_openFileService.OpenFileDialog())
            {
                Attachment newattachment = new Attachment()
                {
                    AttachmentFilename = _openFileService.FileName,
                    AttachmentFile = File.ReadAllBytes(_openFileService.Path)
                };

                _ecnDataService.UpgradeAttachment(attachment.AttachmentId, newattachment.AttachmentFilename, newattachment.AttachmentFile);
                _ = Attachments.Remove(attachment);
                Attachments.Add(newattachment);
            }
        }

        private void VerifiedECN()
        {
            if (_ecnDataService.SignEcn(Ecn, RevisionNotes))
            {
                Employee emp = _ecnDataService.NextToSignEcn(Ecn);

                if (emp != null)
                {
                    _mailService.SendSignEmail(emp.EmployeeEmail, Ecn.Id, emp.Name, Ecn.Employee.Name);
                }
                else
                {
                    if (_ecnDataService.ApproveEcn(Ecn))
                    {
                        _mailService.SendApprovedECN(Ecn.Id, Ecn.Employee.Name);
                    }
                }

                _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se ha validado el ECN.");
                RevisionNotes = string.Empty;
                Ecn.CurrentSignature = null;
                _navigationService.GoBack();
            }
        }

        private void RefuseECN()
        {
            if (_ecnDataService.RefuseEcn(Ecn, RevisionNotes))
            {
                foreach (var item in Revisions)
                {
                    _mailService.SendRefuseECNEmail(item.Employee.EmployeeEmail, Ecn.Id, item.Employee.Name, Ecn.Employee.Name);
                }

                _mailService.SendRefuseECNToGeneratorEmail(Ecn.Employee.EmployeeEmail, Ecn.Id, UserRecord.Employee.Name, Ecn.Employee.Name);

                _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se ha rechazado el ECN. Se le notificara al generador.");
                RevisionNotes = string.Empty;
                Ecn.CurrentSignature = null;
                _navigationService.GoBack();
            }
        }

        private void CancelECN()
        {
            if (_ecnDataService.CancelEcn(Ecn, Notes))
            {
                _mailService.SendCancelECN(Ecn.Employee.EmployeeEmail, Ecn.Id, Ecn.Employee.Name);
                _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "ECN cancelado. Se le notificara al generador.");
                Notes = string.Empty;
                _navigationService.GoBack();

            }
        }

        private void CloseECN()
        {
            if (_ecnDataService.CloseEcn(Ecn, Notes))
            {
                _mailService.SendCloseECN(Ecn.Employee.EmployeeEmail, Ecn.Id, Ecn.Employee.Name);
                _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "ECN cerrado correctamente. Se le notificara al generador.");
                Notes = string.Empty;
                _navigationService.GoBack();
            }
        }

        private void UpgradeECN()
        {
            if (_ecnDataService.UpgradeEcn(Ecn))
            {
                Employee emp = _ecnDataService.FindSigned(Ecn);

                if (emp != null)
                {
                    _mailService.SendSignEmail(emp.EmployeeEmail, Ecn.Id, emp.Name, Ecn.Employee.Name);
                }

                _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se ha modificado el ECN. La persona que rechazo volvera a revisarlo.");
                _navigationService.GoBack();
            }
        }

        private void ExportECN()
        {
            Messenger.Default.Send(new NotificationMessage<Ecn>(Ecn, "ShowReport"));
        }

        private void GoBack()
        {
            _navigationService.GoBack();
        }
    }
}
