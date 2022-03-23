using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class EcnRecordsViewModel : ViewModelBase, INavigationAware
    {
        private IEcnDataService _ecnDataService;
        private INavigationService _navigationService;

        private ICommand _navigateToDetailCommand;
        public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<Ecn>(NavigateToDetail);
        public EcnRecordsViewModel(IEcnDataService ecnDataService, INavigationService navigationService)
        {
            _ecnDataService = ecnDataService;
            _navigationService = navigationService;

            EcnRecords = new CollectionViewSource();

        }

        private ObservableCollection<Ecn> _Records;
        public ObservableCollection<Ecn> Records
        {
            get => _Records;
            set
            {
                if (_Records != value)
                {
                    _Records = value;
                    RaisePropertyChanged("Records");
                }
            }
        }

        private CollectionViewSource _EcnRecords;
        public CollectionViewSource EcnRecords
        {
            get => _EcnRecords;
            set
            {
                if (_EcnRecords != value)
                {
                    _EcnRecords = value;
                    RaisePropertyChanged("EcnRecords");
                }
            }
        }

        private async void GetRecords()
        {
            var data = await _ecnDataService.GetEcnRecordsAsync();

            foreach (var item in data)
            {
                item.ChangeType = await _ecnDataService.GetChangeTypeAsync(item.ChangeTypeId);
                item.DocumentType = await _ecnDataService.GetDocumentTypeAsync(item.DocumentTypeId);
                item.Status = await _ecnDataService.GetStatusAsync(item.StatusId);
                item.Employee = await _ecnDataService.GetEmployeeAsync(item.EmployeeId);

                item.ChangeTypeName = item.ChangeType.ChangeTypeName;
                item.DocumentTypeName = item.DocumentType.DocumentTypeName;
                item.EmployeeName = item.Employee.Name;
                item.StatusName = item.Status.StatusName;

                if (Convert.ToBoolean(item.IsEco))
                {
                    item.EcnEco = await _ecnDataService.GetEcnEcoAsync(item.Id);
                }

                Records.Add(item);
            }
        }

        public void OnNavigatedTo(object parameter)
        {
            Records = new ObservableCollection<Ecn>();
            GetRecords();
            EcnRecords.Source = Records;
        }

        public void OnNavigatedFrom()
        {

        }

        private void NavigateToDetail(Ecn ecn)
        {
            if (ecn != null)
            {
                _navigationService.NavigateTo(typeof(HistoryDetailsViewModel).FullName, ecn);
            }
        }
    }
}
