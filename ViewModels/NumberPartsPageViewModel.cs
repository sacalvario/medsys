
using ECN.Contracts.Services;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class NumberPartsPageViewModel : ViewModelBase 
    {
        public readonly INumberPartsDataService _numberPartsDataService;
        public readonly IWindowManagerService _windowManagerService;
        public readonly IEcnDataService _ecnDataService;

        public NumberPartsPageViewModel(INumberPartsDataService numberPartsDataService, IWindowManagerService windowManagerService, IEcnDataService ecnDataService)
        {
            _numberPartsDataService = numberPartsDataService;
            _windowManagerService = windowManagerService;
            _ecnDataService = ecnDataService;

            GetAll();

            CvsEnfermadades = new CollectionViewSource
            {
                Source = Enfermedades
            };

            CvsEnfermadades.SortDescriptions.Add(new SortDescription("Nombre", ListSortDirection.Ascending));

            EnfermedadesCollection = CvsEnfermadades.View;

            CvsEnfermadades.Filter += ApplyFilter;

            if (UserRecord.Employee_ID == 100 || UserRecord.Employee_ID == 2246 || UserRecord.Employee_ID == 212 || UserRecord.Employee_ID == 39)
            {
                AdminNumberPartsBtnsVisibility = Visibility.Visible;
            }

        }

        private ICommand _OpenNumberPartManageWindowCommand;
        public ICommand OpenNumberPartManageWindowCommand
        {
            get
            {
                if (_OpenNumberPartManageWindowCommand == null)
                {
                    _OpenNumberPartManageWindowCommand = new RelayCommand<Numberpart>(OpenEmployeeManageWindow);
                }
                return _OpenNumberPartManageWindowCommand;
            }
        }

        private ICommand _OpenCustomerManageWindowCommand;
        public ICommand OpenCustomerManageWindowCommand
        {
            get
            {
                if (_OpenCustomerManageWindowCommand == null)
                {
                    _OpenCustomerManageWindowCommand = new RelayCommand(OpenCustomerManagerWindow);
                }
                return _OpenCustomerManageWindowCommand;
            }
        }

        private Visibility _AdminNumberPartsBtnsVisibility = Visibility.Collapsed;
        public Visibility AdminNumberPartsBtnsVisibility
        {
            get => _AdminNumberPartsBtnsVisibility;
            set
            {
                if (_AdminNumberPartsBtnsVisibility != value)
                {
                    _AdminNumberPartsBtnsVisibility = value;
                    RaisePropertyChanged("AdminNumberPartsBtnsVisibility");
                }
            }
        }

        private void OpenCustomerManagerWindow()
        {
            Messenger.Default.Send(new NotificationMessage("ShowManageCustomerWindow"));
        }


        private void OpenEmployeeManageWindow(Numberpart numberpart)
        {
            Messenger.Default.Send(new NotificationMessage<Numberpart>(numberpart, "ShowManageNumberPartWindow"));
        }

        //public async void GetAll()
        //{
        //    NumberParts = new ObservableCollection<Numberpart>();

        //    var data = await _numberPartsDataService.GetNumberPartsAsync();
        //    foreach (var item in data)
        //    {
        //        item.NumberPartTypeNavigation = await _numberPartsDataService.GetNumberpartTypeAsync(item.NumberPartType);
        //        item.Customer = await _numberPartsDataService.GetCustomerAsync(item.CustomerId);
        //        NumberParts.Add(item);
        //    }
        //}

        public async void GetAll()
        {
            Enfermedades = new ObservableCollection<Enfermedad>();

            var data = await _ecnDataService.GetEnfermedadesAsync();
            foreach (var item in data)
            {
                Enfermedades.Add(item);
            }
        }

        private CollectionViewSource _CvsNumberPart;
        internal CollectionViewSource CvsNumberParts
        {
            get => _CvsNumberPart;
            set
            {
                if (_CvsNumberPart != value)
                {
                    _CvsNumberPart = value;
                    RaisePropertyChanged("CvsNumberParts");
                }
            }
        }

        private ICollectionView _NumberPartCollection;
        public ICollectionView NumberPartCollection
        {
            get => _NumberPartCollection;
            set
            {
                if (_NumberPartCollection != value)
                {
                    _NumberPartCollection = value;
                    RaisePropertyChanged("NumberPartCollection");
                }
            }
        }


        private CollectionViewSource _CvsEnfermadades;
        internal CollectionViewSource CvsEnfermadades
        {
            get => _CvsEnfermadades;
            set
            {
                if (_CvsEnfermadades != value)
                {
                    _CvsEnfermadades = value;
                    RaisePropertyChanged("CvsEnfermadades");
                }
            }
        }

        private ICollectionView _EnfermedadesCollection;
        public ICollectionView EnfermedadesCollection
        {
            get => _EnfermedadesCollection;
            set
            {
                if (_EnfermedadesCollection != value)
                {
                    _EnfermedadesCollection = value;
                    RaisePropertyChanged("EnfermedadesCollection");
                }
            }
        }

        private string filter;
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                RaisePropertyChanged("Filter");
                OnFilterChanged();
            }
        }

        private void OnFilterChanged()
        {
            CvsNumberParts.View.Refresh();
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

        private ObservableCollection<Enfermedad> _Enfermedades;
        public ObservableCollection<Enfermedad> Enfermedades
        {
            get => _Enfermedades;
            set
            {
                if (_Enfermedades != value)
                {
                    _Enfermedades = value;
                    RaisePropertyChanged("Enfermedades");
                }
            }
        }


        private Numberpart _NumberPartSelected;
        public Numberpart NumberPartSelected
        {
            get => _NumberPartSelected;
            set
            {
                if (_NumberPartSelected != value)
                {
                    _NumberPartSelected = value;
                    RaisePropertyChanged("NumberPartSelected");
                }
            }
        }

        private void ApplyFilter(object sender, FilterEventArgs e)
        {
            Enfermedad np = (Enfermedad)e.Item;

            e.Accepted = string.IsNullOrWhiteSpace(Filter) || Filter.Length == 0 || np.Nombre.ToLower().Contains(Filter.ToLower());
        }
    }
}
