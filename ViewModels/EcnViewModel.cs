using ECN.Contracts.Services;
using ECN.Contracts.Views;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class EcnViewModel : ViewModelBase
    {

        private readonly IEcnDataService _ecnDataService;
        private readonly IOpenFileService _openFileService;
        private readonly IWindowManagerService _windowManagerService;
        private readonly IMailService _mailService;
        private INumberPartsWindow _numberPartsWindow;
        private IEmployeesWindow _employeesWindow;

        private Ecn _ECN;
        public Ecn ECN
        {
            get => _ECN;
            set
            {
                if (_ECN != value)
                {
                    _ECN = value;
                    RaisePropertyChanged("ECN");
                }
            }
        }
        public RelayCommand SaveECNCommand { get; set; }
        public RelayCommand OpenFileDialogCommand { get; set; }
        public RelayCommand OpenNumberPartsDialogCommand { get; set; }
        public RelayCommand OpenSignatureFlowDialogCommand { get; set; }
        public RelayCommand GoToNextTabItemCommand { get; set; }
        public RelayCommand GoToLastTabItemCommand { get; set; }

        private ICommand _deleteAttachedCommand;
        public ICommand DeleteAttachedCommand => _deleteAttachedCommand ??= new RelayCommand<Attachment>(RemoveAttached);

        private ICommand _deleteNumberPartCommand;
        public ICommand DeleteNumberPartCommand => _deleteNumberPartCommand ??= new RelayCommand<Numberpart>(RemoveNumberPart);

        private ICommand _deleteEmployeeCommand;
        public ICommand DeleteEmployeeCommand => _deleteEmployeeCommand ??= new RelayCommand<Employee>(RemoveEmployee);

        private ICommand _deleteEmployeeForViewCommand;
        public ICommand DeleteEmployeeForViewCommand => _deleteEmployeeForViewCommand ??= new RelayCommand<Employee>(RemoveEmployeeForView);

        private Visibility _EcnEcoVisibility;
        public Visibility EcnEcoVisibility
        {
            get => _EcnEcoVisibility;
            set
            {
                if (_EcnEcoVisibility != value)
                {
                    _EcnEcoVisibility = value;
                    RaisePropertyChanged("EcnEcoVisibility");
                }
            }
        }

        private Visibility _EcnRegisterTypeVisibility;
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

        private Visibility _EcnIntExtTypeVisibility;
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

        private Visibility _EcnUnsubscribeTypeVisibility;
        public Visibility EcnUnsubscribeTypeVisibility
        {
            get => _EcnUnsubscribeTypeVisibility;
            set
            {
                if (_EcnUnsubscribeTypeVisibility != value)
                {
                    _EcnUnsubscribeTypeVisibility = value;
                    RaisePropertyChanged("EcnUnsubscribeTypeVisibility");
                }
            }
        }

        private Visibility _EcnChangeCustomerRevision;
        public Visibility EcnChangeCustomerRevision
        {
            get => _EcnChangeCustomerRevision;
            set
            {
                if (_EcnChangeCustomerRevision != value)
                {
                    _EcnChangeCustomerRevision = value;
                    RaisePropertyChanged("EcnChangeCustomerRevision");
                }
            }
        }


        private Visibility _DocumentTypeVisibility = Visibility.Collapsed;
        public Visibility DocumentTypeVisibility
        {
            get => _DocumentTypeVisibility;
            set
            {
                if (_DocumentTypeVisibility != value)
                {
                    _DocumentTypeVisibility = value;
                    RaisePropertyChanged("DocumentTypeVisibility");
                }
            }
        }


        private Visibility _MainDocumentVisibility = Visibility.Visible;
        public Visibility MainDocumentVisibility
        {
            get => _MainDocumentVisibility;
            set
            {
                if (_MainDocumentVisibility != value)
                {
                    _MainDocumentVisibility = value;
                    RaisePropertyChanged("MainDocumentVisibility");
                }
            }
        }

        private Visibility _DocumentsAffectedVisibility = Visibility.Visible;
        public Visibility DocumentsAffectedVisibility
        {
            get => _DocumentsAffectedVisibility;
            set
            {
                if (_DocumentsAffectedVisibility != value)
                {
                    _DocumentsAffectedVisibility = value;
                    RaisePropertyChanged("DocumentsAffectedVisibility");
                }
            }
        }

        private Visibility _DocumentRevisionsVisibility = Visibility.Visible;
        public Visibility DocumentRevisionsVisibility
        {
            get => _DocumentRevisionsVisibility;
            set
            {
                if (_DocumentRevisionsVisibility != value)
                {
                    _DocumentRevisionsVisibility = value;
                    RaisePropertyChanged("DocumentRevisionsVisibility");
                }
            }
        }

        private Visibility _DocumentRevisionVisibility = Visibility.Collapsed;
        public Visibility DocumentRevisionVisibility
        {
            get => _DocumentRevisionVisibility;
            set
            {
                if (_DocumentRevisionVisibility != value)
                {
                    _DocumentRevisionVisibility = value;
                    RaisePropertyChanged("DocumentRevisionVisibility");
                }
            }
        }

        private Visibility _DocumentNumberVisibility = Visibility.Collapsed;
        public Visibility DocumentNumberVisibility
        {
            get => _DocumentNumberVisibility;
            set
            {
                if (_DocumentNumberVisibility != value)
                {
                    _DocumentNumberVisibility = value;
                    RaisePropertyChanged("DocumentNumberVisibility");
                }
            }
        }

        private Visibility _NewDocumentNumberVisibility = Visibility.Collapsed;
        public Visibility NewDocumentNumberVisibility
        {
            get => _NewDocumentNumberVisibility;
            set
            {
                if (_NewDocumentNumberVisibility != value)
                {
                    _NewDocumentNumberVisibility = value;
                    RaisePropertyChanged("NewDocumentNumberVisibility");
                }
            }
        }

        private Visibility _DocumentNameVisibility = Visibility.Collapsed;
        public Visibility DocumentNameVisibility
        {
            get => _DocumentNameVisibility;
            set
            {
                if (_DocumentNameVisibility != value)
                {
                    _DocumentNameVisibility = value;
                    RaisePropertyChanged("DocumentNameVisibility");
                }
            }
        }

        private Visibility _NewDocumentNameVisibility = Visibility.Collapsed;
        public Visibility NewDocumentNameVisibility
        {
            get => _NewDocumentNameVisibility;
            set
            {
                if (_NewDocumentNameVisibility != value)
                {
                    _NewDocumentNameVisibility = value;
                    RaisePropertyChanged("NewDocumentNameVisibility");
                }
            }
        }

        private Visibility _NumberPartsVisibility = Visibility.Visible;
        public Visibility NumberPartVisibility
        {
            get => _NumberPartsVisibility;
            set
            {
                if (_NumberPartsVisibility != value)
                {
                    _NumberPartsVisibility = value;
                    RaisePropertyChanged("NumberPartsVisibility");
                }
            }
        }

        private Visibility _NumberPartsChangeRevisionVisibility = Visibility.Collapsed;
        public Visibility NumberPartsChangeRevisionVisibility
        {
            get => _NumberPartsChangeRevisionVisibility;
            set
            {
                if (_NumberPartsChangeRevisionVisibility != value)
                {
                    _NumberPartsChangeRevisionVisibility = value;
                    RaisePropertyChanged("NumberPartsChangeRevisionVisibility");
                }
            }
        }

        private Visibility _ChangeRevisionVisibility = Visibility.Collapsed;
        public Visibility ChangeRevisionVisibility
        {
            get => _ChangeRevisionVisibility;
            set
            {
                if (_ChangeRevisionVisibility != value)
                {
                    _ChangeRevisionVisibility = value;
                    RaisePropertyChanged("ChangeRevisionVisibility");
                }
            }
        }

        private bool _IsEco;
        public bool IsEco
        {
            get => _IsEco;
            set
            {
                if (_IsEco != value)
                {
                    _IsEco = value;
                    RaisePropertyChanged("IsEco");
                    EcnEcoVisibility = IsEco ? Visibility.Visible : Visibility.Hidden;
                    ECN.EndDate = IsEco ? DateTime.Now.AddDays(45) : DateTime.Now.AddDays(30);
                }
            }
        }

        private bool _IsNewNumberPartRevision;
        public bool IsNewNumberPartRevision
        {
            get => _IsNewNumberPartRevision;
            set
            {
                if (_IsNewNumberPartRevision != value)
                {
                    _IsNewNumberPartRevision = value;
                    RaisePropertyChanged("IsNewNumberPartRevision");
                    NumberPartsChangeRevisionVisibility = IsNewNumberPartRevision ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private int _RowPosition = 2;
        public int RowPosition
        {
            get => _RowPosition;
            set
            {
                if (_RowPosition != value)
                {
                    _RowPosition = value;
                    RaisePropertyChanged("RowPosition");
                }
            }
        }

        public EcnViewModel(IEcnDataService ecnDataService, IOpenFileService openFileService, IWindowManagerService windowManagerService, IMailService mailService)
        {
            _ecnDataService = ecnDataService;
            _openFileService = openFileService;
            _windowManagerService = windowManagerService;
            _mailService = mailService;

            ECN = new Ecn
            {
                EcnEco = new EcnEco(),
                StartDate = DateTime.Today
            };


            NumberParts = new ObservableCollection<Numberpart>();
            Attacheds = new ObservableCollection<Attachment>();
            SelectedForSign = new ObservableCollection<Employee>();
            SelectedForView = new ObservableCollection<Employee>();

            IsEco = false;

            GetChangeTypes();
            GetDocumentTypes();
            GetEcoTypes();

            SaveECNCommand = new RelayCommand(SaveECN);
            OpenFileDialogCommand = new RelayCommand(OpenFileDialog);
            OpenNumberPartsDialogCommand = new RelayCommand(OpenNumberPartsDialog);
            OpenSignatureFlowDialogCommand = new RelayCommand(OpenSignatureFlowDialog);
            GoToNextTabItemCommand = new RelayCommand(GoToNexTabItem);
            GoToLastTabItemCommand = new RelayCommand(GoToLastTabItem);

            EcnEcoVisibility = Visibility.Collapsed;
            EcnRegisterTypeVisibility = Visibility.Collapsed;
            EcnIntExtTypeVisibility = Visibility.Visible;
            EcnChangeCustomerRevision = Visibility.Collapsed;
            EcnUnsubscribeTypeVisibility = Visibility.Collapsed;

            SelectedForSign.CollectionChanged += SelectedForSignCollectionChanged;
            NumberParts.CollectionChanged += NumberPartCollectionChanged;


        }
        private void SelectedForSignCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in SelectedForSign)
            {
                item.Index = SelectedForSign.IndexOf(item) + 1;
            }
        }

        private void NumberPartCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (NumberParts.Count == 0)
            {
                ViewModelLocator.UnregisterNumberPartViewModel();
                ECN.OldDrawingLvl = string.Empty;

                if (ChangeRevisionVisibility == Visibility.Visible)
                {
                    ChangeRevisionVisibility = Visibility.Collapsed;
                }
            }
            else if (NumberParts.Count == 1)
            {
                ECN.OldDrawingLvl = NumberParts[0].NumberPartRev;

                if (SelectedChangeType != null)
                {
                    if (SelectedChangeType.ChangeTypeId < 3)
                    {
                        ChangeRevisionVisibility = Visibility.Visible;
                    }
                }
            }
            else if (NumberParts.Count == 11)
            {
                NumberParts.CollectionChanged -= NumberPartCollectionChanged;
                RemoveNumberPart();
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Solo se permiten 10 número de parte");
                NumberParts.CollectionChanged += NumberPartCollectionChanged;
            }
        }

        private void RemoveNumberPart()
        {
            NumberParts.RemoveAt(10);
        }

        private void SaveECN()
        {
            if (SelectedChangeType != null)
            {
                ECN.ChangeType = SelectedChangeType;

                if (ECN.ChangeType.ChangeTypeId != 3)
                {
                    if (IsEco)
                    {
                        if (!string.IsNullOrWhiteSpace(ECN.EcnEco.IdEco) && !string.IsNullOrWhiteSpace(ECN.EcnEco.EcoTypeId.ToString()))
                        {
                            SaveChangeRevisionECN();
                        }
                        else
                        {
                            _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Faltan campos por llenar");
                        }
                    }
                    else
                    {
                        SaveChangeRevisionECN();
                    }
                }

                else if (ECN.ChangeType.ChangeTypeId == 3)
                {
                    if (IsEco)
                    {
                        if (!string.IsNullOrWhiteSpace(ECN.EcnEco.IdEco) && !string.IsNullOrWhiteSpace(ECN.EcnEco.EcoTypeId.ToString()))
                        {
                            SaveRegisterDocumentECN();
                        }
                        else
                        {
                            _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Faltan campos por llenar");
                        }
                    }
                    else
                    {
                        SaveRegisterDocumentECN();
                    }
                }
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "No has seleccionado un tipo de cambio");
            }
        }


        private void SaveChangeRevisionECN()
        {
            if (SelectedDocumentType != null && !string.IsNullOrWhiteSpace(ECN.OldDocumentLvl) && !string.IsNullOrWhiteSpace(ECN.DocumentLvl) && !string.IsNullOrWhiteSpace(ECN.ChangeDescription) && !string.IsNullOrWhiteSpace(ECN.ChangeJustification) && !string.IsNullOrWhiteSpace(ECN.ManufacturingAffectations))
            {
                if (Attacheds.Count != 0)
                {
                    if (SelectedForSign.Count != 0)
                    {
                        ECN.DocumentName = "N/A";
                        ECN.DocumentNo = "N/A";
                        ECN.DocumentType = SelectedDocumentType;

                        if (ECN.DocumentType.DocumentTypeId == 2 || ECN.DocumentType.DocumentTypeId == 4 || ECN.DocumentType.DocumentTypeId == 15 || ECN.DocumentType.DocumentTypeId == 16 || ECN.DocumentType.DocumentTypeId == 17)
                        {
                            if (ECN.DrawingLvl != null && ECN.OldDrawingLvl != null)
                            {
                                RegisterECN();
                            }
                            else
                            {
                                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Faltan campos por llenar");
                            }
                        }
                        else
                        {
                            ECN.OldDrawingLvl = "N/A";
                            ECN.DrawingLvl = "N/A";
                            RegisterECN();
                        }
                    }
                    else
                    {
                        _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "No has seleccionado un flujo de firmas");
                    }

                }
                else
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "No has adjuntando archivos");
                }
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Faltan campos por llenar");
            }
        }


        private void SaveRegisterDocumentECN()
        {
            if (SelectedRegisterDocumentType != null && !string.IsNullOrWhiteSpace(ECN.DocumentLvl) && !string.IsNullOrWhiteSpace(ECN.DocumentName) && !string.IsNullOrWhiteSpace(ECN.DocumentNo) && !string.IsNullOrWhiteSpace(ECN.ChangeDescription) && !string.IsNullOrWhiteSpace(ECN.ChangeJustification) && !string.IsNullOrWhiteSpace(ECN.ManufacturingAffectations))
            {
                if (Attacheds.Count != 0)
                {
                    if (SelectedForSign.Count != 0)
                    {
                        ECN.DocumentType = SelectedRegisterDocumentType;
                        ECN.OldDocumentLvl = "N/A";
                        ECN.OldDrawingLvl = "N/A";
                        ECN.DrawingLvl = "N/A";
                        RegisterECN();

                    }
                    else
                    {
                        _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "No has seleccionado un flujo de firmas");
                    }

                }
                else
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "No has adjuntando archivos");
                }
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Faltan campos por llenar");
            }
        }


        private void RegisterECN()
        {
            ECN.EmployeeId = UserRecord.Employee_ID;
            ECN.StatusId = 5;

            if (IsEco)
            {
                ECN.IsEco = Convert.ToSByte(IsEco);
            }
            else
            {
                ECN.EcnEco = null;
            }

            foreach (Documenttype dt in DocumentTypes)
            {
                if (dt.IsSelected && dt != ECN.DocumentType)
                {
                    ECN.EcnDocumenttypes.Add(new EcnDocumenttype()
                    {
                        EcnId = ECN.Id,
                        DocumentTypeId = dt.DocumentTypeId
                    });
                }
            }

            if (Attacheds.Count > 0)
            {
                foreach (Attachment ar in Attacheds)
                {
                    ECN.EcnAttachments.Add(new EcnAttachment()
                    {
                        Attachment = ar,
                        EcnId = ECN.Id
                    });
                }
            }

            if (NumberParts.Count > 0)
            {
                foreach (Numberpart np in NumberParts)
                {
                    ECN.EcnNumberparts.Add(new EcnNumberpart()
                    {
                        Ecn = ECN,
                        ProductId = np.NumberPartNo
                    });
                }
            }

            if (SelectedForSign.Count > 0)
            {
                foreach (Employee er in SelectedForSign)
                {
                    EcnRevision revision = new EcnRevision
                    {
                        Ecn = ECN,
                        Employee = er,
                        RevisionSequence = er.Index,
                        StatusId = SetStatus(er),
                        Notes = ""
                    };
                    ECN.EcnRevisions.Add(revision);
                }
            }
            try
            {
                if (_ecnDataService.SaveEcn(ECN))
                {

                    _mailService.SendSignEmail(SelectedForSign[0].EmployeeEmail, ECN.Employee.EmployeeEmail, ECN.Id, SelectedForSign[0].Name, ECN.Employee.Name);

                    if (SelectedForView.Count > 0)
                    {
                        List<string> emails = new List<string>();
                        foreach (var item in SelectedForView)
                        {
                            emails.Add(item.EmployeeEmail);
                        }
                        _mailService.SendEmail(emails, ECN.Id);
                    }
                    _ = _windowManagerService.OpenInDialog(typeof(EcnRegistrationViewModel).FullName, ECN.Id);
                    ResetData();
                    SelectedTabItem = 0;
                    ViewModelLocator.UnregisterNumberPartViewModel();
                    ViewModelLocator.UnregisterEmployeesViewModel();
                }

            }
            catch (Exception ex)
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al registrar - " + ex.ToString());
            }
        }


        private int SetStatus(Employee er)
        {
            return SelectedForSign.IndexOf(er) == 0 ? 5 : 1;
        }

        private void OpenFileDialog()
        {
            if (_openFileService.OpenFileDialog())
            {
                Attacheds.Add(new Attachment()
                {
                    AttachmentFilename = _openFileService.FileName,
                    AttachmentFile = File.ReadAllBytes(_openFileService.Path)
                });
            }
        }

        private void OpenNumberPartsDialog()
        {
            if (Application.Current.Windows.OfType<INumberPartsWindow>().Count() == 0)
            {
                _numberPartsWindow = SimpleIoc.Default.GetInstance<INumberPartsWindow>(Guid.NewGuid().ToString());
                _numberPartsWindow.ShowWindow();
            }
        }

        private void RemoveNumberPart(Numberpart numberpart)
        {
            if (numberpart != null)
            {
                _ = NumberParts.Remove(numberpart);
            }
        }

        private void RemoveAttached(Attachment attachment)
        {
            if (attachment != null)
            {
                _ = Attacheds.Remove(attachment);
            }
        }
        
        private void RemoveEmployee(Employee employee)
        {
            if (employee != null)
            {
                _ = SelectedForSign.Remove(employee);
            }
        }

        private void RemoveEmployeeForView(Employee employee)
        {
            if (employee != null)
            {
                _ = SelectedForView.Remove(employee);
            }
        }

        private void OpenSignatureFlowDialog()
        {
            if (Application.Current.Windows.OfType<IEmployeesWindow>().Count() == 0)
            {
                ViewModelLocator.UnregisterEmployeesViewModel();
                _employeesWindow = SimpleIoc.Default.GetInstance<IEmployeesWindow>(Guid.NewGuid().ToString());
                _employeesWindow.ShowWindow();
            }
        }

        private void ResetData()
        {
            ECN = new Ecn
            {
                EcnEco = new EcnEco(),
                StartDate = DateTime.Today
            };

            if (IsEco)
            {
                IsEco = false;
            }

            SelectedChangeType = null;
            SelectedDocumentType = null;
            SelectedRegisterDocumentType = null;

            SelectedChangeType = new Changetype();
            SelectedDocumentType = new Documenttype();
            SelectedRegisterDocumentType = new Documenttype();
            Attacheds = new ObservableCollection<Attachment>();
            NumberParts = new ObservableCollection<Numberpart>();
            SelectedForSign = new ObservableCollection<Employee>();
            SelectedForView = new ObservableCollection<Employee>();

            foreach (Documenttype dt in DocumentTypes)
            {
                if (dt.IsSelected)
                {
                    dt.IsSelected = false;
                }
            }

            SelectedForSign.CollectionChanged += SelectedForSignCollectionChanged;
            NumberParts.CollectionChanged += NumberPartCollectionChanged;
        }

        private void MakeVisibleIntExtData()
        {
            DocumentsAffectedVisibility = Visibility.Visible;
            MainDocumentVisibility = Visibility.Visible;
            DocumentRevisionsVisibility = Visibility.Visible;

            if (NumberPartVisibility == Visibility.Collapsed)
            {
                NumberPartVisibility = Visibility.Visible;
            }

        }

        private void MakeVisibleDeleteData()
        {
            if (DocumentRevisionsVisibility == Visibility.Visible)
            {
                DocumentRevisionsVisibility = Visibility.Collapsed;
            }
        }

        private void MakeVisibleRegisterData()
        {
            DocumentTypeVisibility = Visibility.Visible;
            NewDocumentNumberVisibility = Visibility.Visible;
            NewDocumentNameVisibility = Visibility.Visible;
            DocumentRevisionVisibility = Visibility.Visible;

            if (NumberPartVisibility == Visibility.Collapsed)
            {
                NumberPartVisibility = Visibility.Visible;
            }

            DocumentsAffectedVisibility = Visibility.Collapsed;
            MainDocumentVisibility = Visibility.Collapsed;
            DocumentRevisionsVisibility = Visibility.Collapsed;
        }
        private void GoToNexTabItem()
        {
            SelectedTabItem++;
        }
        private void GoToLastTabItem()
        {
            SelectedTabItem--;
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

        private ObservableCollection<Changetype> _ChangeTypes;
        public ObservableCollection<Changetype> ChangeTypes
        {
            get => _ChangeTypes;
            set
            {
                if (_ChangeTypes != value)
                {
                    _ChangeTypes = value;
                    RaisePropertyChanged("ChangeTypes");
                }
            }
        }

        private ObservableCollection<Documenttype> _DocumentTypes;
        public ObservableCollection<Documenttype> DocumentTypes
        {
            get => _DocumentTypes;
            set
            {
                if (_DocumentTypes != value)
                {
                    _DocumentTypes = value;

                    RaisePropertyChanged("DocumentTypes");
                }
            }
        }

        private ObservableCollection<EcoType> _EcoTypes;
        public ObservableCollection<EcoType> EcoTypes
        {
            get => _EcoTypes;
            set
            {
                if (_EcoTypes != value)
                {
                    _EcoTypes = value;
                    RaisePropertyChanged("EcoTypes");
                }
            }
        }

        private ObservableCollection<Attachment> _Attacheds;
        public ObservableCollection<Attachment> Attacheds
        {
            get => _Attacheds;
            set
            {
                if (_Attacheds != value)
                {
                    _Attacheds = value;
                    RaisePropertyChanged("Attacheds");
                }
            }
        }

        private Changetype _SelectedChangeType;
        public Changetype SelectedChangeType
        {
            get => _SelectedChangeType;
            set
            {
                if (_SelectedChangeType != value)
                {
                    _SelectedChangeType = value;
                    RaisePropertyChanged("SelectedChangeType");

                    if (SelectedChangeType != null)
                    {
                        if (_SelectedChangeType.ChangeTypeId == 3)
                        {
                            MakeVisibleRegisterData();
                        }
                        else if (_SelectedChangeType.ChangeTypeId == 4)
                        {
                            MakeVisibleDeleteData();
                        }
                        else if (_SelectedChangeType.ChangeTypeId < 3)
                        {
                            if (EcnIntExtTypeVisibility == Visibility.Collapsed)
                            {
                                EcnIntExtTypeVisibility = Visibility.Visible;
                                EcnRegisterTypeVisibility = Visibility.Collapsed;
                                EcnUnsubscribeTypeVisibility = Visibility.Collapsed;
                            }
                        }
                        else
                        {
                            if (DocumentRevisionVisibility == Visibility.Visible)
                            {
                                DocumentRevisionVisibility = Visibility.Collapsed;
                                EcnIntExtTypeVisibility = Visibility.Collapsed;
                                EcnRegisterTypeVisibility = Visibility.Collapsed;
                            }
                        }
                    }
                }
            }
        }

        private Documenttype _SelectedDocumentType;
        public Documenttype SelectedDocumentType
        {
            get => _SelectedDocumentType;
            set
            {
                if (_SelectedDocumentType != value)
                {
                    _SelectedDocumentType = value;
                    RaisePropertyChanged("SelectedDocumentType");

                    if (_SelectedDocumentType != null)
                    {
                        if (SelectedDocumentType.DocumentTypeId == 1 || SelectedDocumentType.DocumentTypeId == 5 || SelectedDocumentType.DocumentTypeId == 6 || SelectedDocumentType.DocumentTypeId == 7 || SelectedDocumentType.DocumentTypeId == 10 || SelectedDocumentType.DocumentTypeId == 11 || SelectedDocumentType.DocumentTypeId == 12 || SelectedDocumentType.DocumentTypeId == 13 || SelectedDocumentType.DocumentTypeId == 19)
                        {
                            DocumentNameVisibility = Visibility.Visible;
                            DocumentNumberVisibility = Visibility.Visible;
                            RowPosition = 1;
                        }
                        else
                        {
                            if (DocumentNameVisibility == Visibility.Visible)
                            {
                                DocumentNameVisibility = Visibility.Collapsed;
                                DocumentNumberVisibility = Visibility.Collapsed;
                                RowPosition = 2;
                            }
                        }
                    }
                }
            }
        }

        private Documenttype _SelectedRegisterDocumentType;
        public Documenttype SelectedRegisterDocumentType
        {
            get => _SelectedRegisterDocumentType;
            set
            {
                if (_SelectedRegisterDocumentType != value)
                {
                    _SelectedRegisterDocumentType = value;
                    RaisePropertyChanged("SelectedRegisterDocumentType");
                }
            }
        }

        private ObservableCollection<Employee> _SelectedForView;
        public ObservableCollection<Employee> SelectedForView
        {
            get => _SelectedForView;
            set
            {
                if (_SelectedForView != value)
                {
                    _SelectedForView = value;
                    RaisePropertyChanged("SelectedForView");
                }
            }
        }

        private ObservableCollection<Employee> _SelectedForSign;
        public ObservableCollection<Employee> SelectedForSign
        {
            get => _SelectedForSign;
            set
            {
                if (_SelectedForSign != value)
                {
                    _SelectedForSign = value;
                    RaisePropertyChanged("SelectedForSign");
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


        public async void GetChangeTypes()
        {
            ChangeTypes = new ObservableCollection<Changetype>();

            var data = await _ecnDataService.GetChangeTypesAsync();
            foreach(var item in data)
            {
                ChangeTypes.Add(item);
            }
        }

        public async void GetDocumentTypes()
        {
            DocumentTypes = new ObservableCollection<Documenttype>();

            var data = await _ecnDataService.GetDocumentTypesAsync();
            foreach (var item in data)
            {
                DocumentTypes.Add(item);
            }
        }

        public async void GetEcoTypes()
        {
            EcoTypes = new ObservableCollection<EcoType>();

            var data = await _ecnDataService.GetEcoTypesAsync();
            foreach (var item in data)
            {
                EcoTypes.Add(item);
            }

        }

    }
}
