using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace ECN.ViewModels
{
    public class ChecklistViewModel : ViewModelBase, INavigationAware
    {
        private readonly IEcnDataService _ecnDataService;
        private readonly INavigationService _navigationService;
        private ICommand _navigateToCheckCommand;

        public ICommand NavigateToCheckCommand => _navigateToCheckCommand ??= new RelayCommand<Ecn>(NavigateToCheck);

        public ChecklistViewModel(IEcnDataService ecnDataService, INavigationService navigationService)
        {
            _ecnDataService = ecnDataService;
            _navigationService = navigationService;

        }

        private ObservableCollection<Cita> _Checklist;
        public ObservableCollection<Cita> Checklist
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

        //private ObservableCollection<Ecn> _Checklist;
        //public ObservableCollection<Ecn> Checklist
        //{
        //    get => _Checklist;
        //    set
        //    {
        //        if (_Checklist != value)
        //        {
        //            _Checklist = value;
        //            RaisePropertyChanged("Checklist");
        //        }
        //    }
        //}

        private int _ChecklistCount;
        public int ChecklistCount
        {
            get => _ChecklistCount;
            set
            {
                if (_ChecklistCount != value)
                {
                    _ChecklistCount = value;
                    RaisePropertyChanged("ChecklistCount");
                }
            }
        }


        private async void GetChecklist()
        {
            var data = await _ecnDataService.GetCitasPendientesAsync();

            foreach (var item in data)
            {
                item.IdPacienteNavigation = await _ecnDataService.GetPacienteAsync(item.IdPaciente);
                item.IdEstadoNavigation = await _ecnDataService.GetEstadoAsync(item.IdEstado);

                Checklist.Add(item);
            }
        }

        private void NavigateToCheck(Ecn ecn)
        {
            _navigationService.NavigateTo(typeof(HistoryDetailsViewModel).FullName, ecn);
        }

        public void OnNavigatedTo(object parameter)
        {
            Checklist = new ObservableCollection<Cita>();
            GetChecklist();

            ChecklistCount = Checklist.Count;

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(15)
            };
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Checklist = new ObservableCollection<Cita>();
            GetChecklist();

            ChecklistCount = Checklist.Count;
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
