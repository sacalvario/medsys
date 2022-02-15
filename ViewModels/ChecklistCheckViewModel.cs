using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.IO;

namespace ECN.ViewModels
{
    public class ChecklistCheckViewModel : ViewModelBase, INavigationAware
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

        private RelayCommand _SignCommand;
        public RelayCommand SignCommand
        {
            get
            {
                if (_SignCommand == null)
                {
                    _SignCommand = new RelayCommand(SignEcn);
                }
                return _SignCommand;
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


        public ChecklistCheckViewModel(IEcnDataService ecnDataService, INumberPartsDataService numberPartsDataService)
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

            NumberParts = new ObservableCollection<Numberpart>();
            Attachments = new ObservableCollection<Attachment>();
            Revisions = new ObservableCollection<EcnRevision>();
            GetNumberParts();
            GetAttachments();
            GetRevisions();

        }

        private void SignEcn()
        {
            //_ecnDataService.SignEcn(Ecn);
        }

        private async void GetNumberParts()
        {
            Ecn.EcnNumberparts = await _numberPartsDataService.GetNumberPartsEcnsAsync(Ecn.Id);

            foreach (var item in Ecn.EcnNumberparts)
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
    }
}
