using ECN.Contracts.Services;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
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


        public EcnViewModel(IEcnDataService ecnDataService, IOpenFileService openFileService, IWindowManagerService windowManagerService, IMailService mailService)
        {
             _ecnDataService = ecnDataService;
            _openFileService = openFileService;
            _windowManagerService = windowManagerService;
            _mailService = mailService;

            ECN = new Ecn
            {
                EcnEco = new EcnEco()
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

            SelectedForSign.CollectionChanged += FullObservableCollectionChanged;

            //SelectedTabItem = 1;

        }
        private void FullObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach(var item in SelectedForSign)
            {
                item.Index = SelectedForSign.IndexOf(item) + 1;
            }
        }

        private void SaveECN()
        {
            if (ECN != null)
            {
                ECN.ChangeType = SelectedChangeType;

                if (ECN.ChangeType.ChangeTypeId != 3)
                {
                    ECN.DocumentName = "N/A";
                    ECN.DocumentNo = "N/A";
                    ECN.DocumentType = SelectedDocumentType;
                }

                else if (ECN.ChangeType.ChangeTypeId == 3)
                {
                    ECN.DocumentType = SelectedRegisterDocumentType;
                    ECN.OldDocumentLvl = "N/A";
                    ECN.OldDrawingLvl = "N/A";
                    ECN.DrawingLvl = "N/A";
                }

                ECN.EmployeeId = UserRecord.Employee_ID;
                ECN.StatusId = 1;

                if (IsEco)
                {
                    ECN.IsEco = Convert.ToSByte(IsEco);
                }
                else
                {
                    ECN.EcnEco = null;
                }

                try
                {

                    if (_ecnDataService.SaveEcn(ECN))
                    {
                       
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
                                    EcnId = ECN.Id,
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

                        _ecnDataService.SaveChanges();

                        _mailService.SendSignEmail(SelectedForSign[0].EmployeeEmail, ECN.Id, SelectedForSign[0].Name);
                        foreach (Employee er in SelectedForView)
                        {
                            _mailService.SendEmail(er.EmployeeEmail, ECN.Id, er.Name);
                        }

                        _ = _windowManagerService.OpenInDialog(typeof(EcnRegistrationViewModel).FullName, ECN.Id);
                        ResetData();
                        SelectedTabItem = 0;
                    }

                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show("Error occured while saving. " + ex.ToString());
                }
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
                Attacheds.Add(new Attachment(Path.GetExtension(_openFileService.Path))
                {
                    AttachmentFilename = _openFileService.FileName,
                    AttachmentFile = File.ReadAllBytes(_openFileService.Path)
                });
            }
        }

        private void OpenNumberPartsDialog()
        {
            Messenger.Default.Send(new NotificationMessage("ShowNumberParts"));
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

        private void OpenSignatureFlowDialog()
        {
            Messenger.Default.Send(new NotificationMessage("ShowEmployees"));
        }

        private void ResetData()
        {
            ECN = new Ecn
            {
                EcnEco = new EcnEco()
            };

            if (IsEco)
            {
                IsEco = false;
            }

            SelectedChangeType = new Changetype();
            SelectedDocumentType = new Documenttype();
            SelectedRegisterDocumentType = new Documenttype();
            Attacheds = new ObservableCollection<Attachment>();
            NumberParts = new ObservableCollection<Numberpart>();
            SelectedForSign = new ObservableCollection<Employee>();
            SelectedForView = new ObservableCollection<Employee>();
            GetDocumentTypes();

            SelectedForSign.CollectionChanged += FullObservableCollectionChanged;
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

        private Numberpart _SelectedNumberPart;
        public Numberpart SelectedNumberPart
        {
            get => _SelectedNumberPart;
            set
            {
                if (_SelectedNumberPart != value)
                {
                    _SelectedNumberPart = value;
                    RaisePropertyChanged("SelectedNumberPart");

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

                    if (_SelectedChangeType.ChangeTypeId == 3)
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
