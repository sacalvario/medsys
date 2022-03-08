using ECN.Contracts.Services;
using ECN.Models;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace ECN.ViewModels
{
    public class NumberPartsViewModel : ViewModelBase
    {
        private readonly INumberPartsDataService _numberPartsDataService;

        private Visibility _FilterDataVisibility = Visibility.Collapsed;
        public Visibility FilterDataVisibility
        {
            get => _FilterDataVisibility;
            set
            {
                if (_FilterDataVisibility != value)
                {
                    _FilterDataVisibility = value;
                    RaisePropertyChanged("FilterDataVisibility");
                }
            }
        }

        public NumberPartsViewModel(INumberPartsDataService numberPartsDataService)
        {
            _numberPartsDataService = numberPartsDataService;
            GetAll();

            CvsNumberParts = new CollectionViewSource
            {
                Source = NumberParts
            };

            CvsNumberParts.SortDescriptions.Add(new SortDescription("Customer.CustomerName", ListSortDirection.Ascending));
            CvsNumberParts.SortDescriptions.Add(new SortDescription("NumberPartId", ListSortDirection.Ascending));

            NumberPartCollection = CvsNumberParts.View;

            CvsNumberParts.Filter += ApplyFilter;

            CvsNumberParts.View.CollectionChanged += NumberPartCollectionChanged;

        }

        private void NumberPartCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                if (e.OldItems.Count > 0)
                {
                    if (!CvsNumberParts.View.Contains(SelectedNumberPart))
                    {
                        Customer = SelectedNumberPart.Customer.CustomerName;
                        Revision = SelectedNumberPart.NumberPartRev;

                        GetPerCustomer(SelectedNumberPart.Customer.CustomerId, SelectedNumberPart.NumberPartRev);
                        _ = NumberParts.Remove(SelectedNumberPart);
                        CvsNumberParts = new CollectionViewSource
                        {
                            Source = NumberParts
                        };

                        NumberPartCollection = CvsNumberParts.View;
                        FilterDataVisibility = Visibility.Visible;
                        CvsNumberParts.Filter += ApplyFilter;

                        CvsNumberParts.View.CollectionChanged -= NumberPartCollectionChanged;
                    }
                }
            }
        }

        public async void GetAll()
        {
            NumberParts = new ObservableCollection<Numberpart>();

            var data = await _numberPartsDataService.GetNumberPartsAsync();
            foreach(var item in data)
            {
                item.NumberPartTypeNavigation = await _numberPartsDataService.GetNumberpartTypeAsync(item.NumberPartType);
                item.Customer = await _numberPartsDataService.GetCustomerAsync(item.CustomerId);
                NumberParts.Add(item);
            }
        }

        public async void GetPerCustomer(int customerid, string revision)
        {
            NumberParts = new ObservableCollection<Numberpart>();

            var data = await _numberPartsDataService.GetNumberPartsPerCustomerAsync(customerid, revision);
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

        private string customer;
        public string Customer
        {
            get => customer;
            set
            {
                customer = value;
                RaisePropertyChanged("Customer");
            }
        }


        private string revision;
        public string Revision
        {
            get => revision;
            set
            {
                revision = value;
                RaisePropertyChanged("Revision");
                
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
