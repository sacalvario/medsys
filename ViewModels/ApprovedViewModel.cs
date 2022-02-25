using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class ApprovedViewModel : ViewModelBase, INavigationAware
    {
        private IEcnDataService _ecnDataService;
        private INavigationService _navigationService;
        private ICommand _navigateToCheckCommand;

        public ICommand NavigateToCheckCommand => _navigateToCheckCommand ??= new RelayCommand<Ecn>(NavigateToCheck);
        public ApprovedViewModel(IEcnDataService ecnDataService, INavigationService navigationService)
        {
            _ecnDataService = ecnDataService;
            _navigationService = navigationService;
        }

        private ObservableCollection<Ecn> _Approved;
        public ObservableCollection<Ecn> Approved
        {
            get => _Approved;
            set
            {
                if (_Approved != value)
                {
                    _Approved = value;
                    RaisePropertyChanged("Approved");
                }
            }
        }

        private int _ApprovedtCount;
        public int ApprovedCount
        {
            get => _ApprovedtCount;
            set
            {
                if (_ApprovedtCount != value)
                {
                    _ApprovedtCount = value;
                    RaisePropertyChanged("ApprovedCount");
                }
            }
        }


        private async void GetApproved()
        {
            var data = await _ecnDataService.GetApprovedAsync();

            foreach (var item in data)
            {
                item.ChangeType = await _ecnDataService.GetChangeTypeAsync(item.ChangeTypeId);
                item.DocumentType = await _ecnDataService.GetDocumentTypeAsync(item.DocumentTypeId);
                item.Status = await _ecnDataService.GetStatusAsync(item.StatusId);
                item.Employee = await _ecnDataService.GetEmployeeAsync(item.EmployeeId);

                if (Convert.ToBoolean(item.IsEco))
                {
                    item.EcnEco = await _ecnDataService.GetEcnEcoAsync(item.Id);
                }

                Approved.Add(item);
            }
        }

        private void NavigateToCheck(Ecn ecn)
        {
            _navigationService.NavigateTo(typeof(HistoryDetailsViewModel).FullName, ecn);
        }

        public void OnNavigatedTo(object parameter)
        {
            Approved = new ObservableCollection<Ecn>();
            GetApproved();

            ApprovedCount = Approved.Count;
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
