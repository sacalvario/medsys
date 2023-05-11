
using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
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

        private Cita _Cita;
        public Cita Cita
        {
            get => _Cita;
            set
            {
                if (_Cita != value)
                {
                    _Cita = value;
                    RaisePropertyChanged("Cita");
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

        private ObservableCollection<Sintoma> _Sintomas;
        public ObservableCollection<Sintoma> Sintomas
        {
            get => _Sintomas;
            set
            {
                if (_Sintomas != value)
                {
                    _Sintomas = value;
                    RaisePropertyChanged("Sintomas");
                }
            }
        }


        //private ICommand _CloseECNCommand;
        //public ICommand CloseECNCommand
        //{
        //    get
        //    {
        //        if (_CloseECNCommand == null)
        //        {
        //            _CloseECNCommand = new RelayCommand(CloseECN);
        //        }
        //        return _CloseECNCommand;
        //    }
        //}

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
            _ecnDataService = ecnDataService;
            _numberPartsDataService = numberPartsDataService;
            _openFileService = openFileService;
            _windowManagerService = windowManagerService;
            _mailService = mailService;
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom()
        {
          
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Cita cita)
            {
                Cita = new Cita();
                Cita = cita;
            }

            NumberParts = new ObservableCollection<Numberpart>();
            Attachments = new ObservableCollection<Attachment>();
            Revisions = new ObservableCollection<EcnRevision>();
            Documents = new ObservableCollection<EcnDocumenttype>();
            Sintomas = new ObservableCollection<Sintoma>();
            //GetNumberParts();
            //GetAttachments();
            //GetRevisions();
            //GetDocuments();

           
        }

        private async void GetSintomas()
        {
            var sintomas = await _ecnDataService.GetSintomasAsync();

            foreach (var item in sintomas)
            {
                Sintomas.Add(item);
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

       
        //private void CloseECN()
        //{
        //    if (_ecnDataService.CloseEcn(Ecn, Notes))
        //    {
        //        _mailService.SendCloseECN(Ecn.Employee.EmployeeEmail, Ecn.Id, Ecn.Employee.Name);

        //        if (Convert.ToBoolean(Ecn.IsEco))
        //        {
        //            _mailService.SendCloseECO(Ecn.Id, Ecn.Employee.Name, Ecn.Employee.EmployeeEmail);
        //        }

        //        _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "ECN cerrado correctamente. Se le notificara al generador.");
        //        Notes = string.Empty;
        //        _navigationService.GoBack();
        //    }
        //}

        private void GoBack()
        {
            _navigationService.GoBack();
        }
    }
}
