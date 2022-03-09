using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace ECN.ViewModels
{
    public class EcnRecordsViewModel : ViewModelBase, INavigationAware
    {
        private IEcnDataService _ecnDataService;
        public EcnRecordsViewModel(IEcnDataService ecnDataService)
        {
            _ecnDataService = ecnDataService;

            Records = new ObservableCollection<Ecn>();
            GetRecords();
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
            EcnRecords = new CollectionViewSource
            {
                Source = Records
            };
        }

        public void OnNavigatedFrom()
        {

        }
    }
}
