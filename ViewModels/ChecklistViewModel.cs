using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class ChecklistViewModel : ViewModelBase, INavigationAware
    {
        private IEcnDataService _ecnDataService;
        private INavigationService _navigationService;
        private ICommand _navigateToCheckCommand;

        public ICommand NavigateToCheckCommand => _navigateToCheckCommand ??= new RelayCommand<Ecn>(NavigateToCheck);

        public ChecklistViewModel(IEcnDataService ecnDataService, INavigationService navigationService)
        {
            _ecnDataService = ecnDataService;
            _navigationService = navigationService;

            Checklist = new ObservableCollection<Ecn>();
            GetChecklist();
        }

        private ObservableCollection<Ecn> _Checklist;
        public ObservableCollection<Ecn> Checklist
        {
            get => _Checklist;
            set
            {
                if (_Checklist != value)
                {
                    _Checklist = value;
                    RaisePropertyChanged("Checklist");
                }
            }
        }

        public int ChecklistCount => Checklist.Count;

        private async void GetChecklist()
        {
            var data = await _ecnDataService.GetChecklistAsync();

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

                Checklist.Add(item);
            }
        }

        private void NavigateToCheck(Ecn ecn)
        {
            _navigationService.NavigateTo(typeof(HistoryDetailsViewModel).FullName, ecn);
        }

        public void OnNavigatedTo(object parameter)
        {
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
