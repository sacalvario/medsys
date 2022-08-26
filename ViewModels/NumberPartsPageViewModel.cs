
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
        public NumberPartsPageViewModel(INumberPartsDataService numberPartsDataService, IWindowManagerService windowManagerService)
        {
            _numberPartsDataService = numberPartsDataService;
            _windowManagerService = windowManagerService;

            GetAll();

            CvsNumberParts = new CollectionViewSource
            {
                Source = NumberParts
            };

            CvsNumberParts.SortDescriptions.Add(new SortDescription("Customer.CustomerName", ListSortDirection.Ascending));
            CvsNumberParts.SortDescriptions.Add(new SortDescription("NumberPartId", ListSortDirection.Ascending));

            NumberPartCollection = CvsNumberParts.View;

            CvsNumberParts.Filter += ApplyFilter;

            if (UserRecord.Employee_ID == 100 || UserRecord.Employee_ID == 5295)
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


        private void OpenEmployeeManageWindow(Numberpart numberpart)
        {
            Messenger.Default.Send(new NotificationMessage<Numberpart>(numberpart, "ShowManageNumberPartWindow"));
        }

        public async void GetAll()
        {
            NumberParts = new ObservableCollection<Numberpart>();

            var data = await _numberPartsDataService.GetNumberPartsAsync();
            foreach (var item in data)
            {
                item.NumberPartTypeNavigation = await _numberPartsDataService.GetNumberpartTypeAsync(item.NumberPartType);
                item.Customer = await _numberPartsDataService.GetCustomerAsync(item.CustomerId);
                NumberParts.Add(item);
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
            Numberpart np = (Numberpart)e.Item;

            e.Accepted = string.IsNullOrWhiteSpace(Filter) || Filter.Length == 0 || np.NumberPartId.ToLower().Contains(Filter.ToLower());
        }
    }
}
