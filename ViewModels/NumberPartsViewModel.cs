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

            CvsNumberParts.Filter += ApplyFilter;
           
            NumberPartCollection.CollectionChanged += NumberPartCollectionChanged;
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
                        //NumberPartCollection.CollectionChanged += null;
                    }
                }
            }
            //else
            //{
            //    return;
            //}
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

        internal CollectionViewSource CvsNumberParts { get; set; }
        public ICollectionView NumberPartCollection => CvsNumberParts.View;

        private string filter;
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                OnFilterChanged();
            }
        }

        public string Customer { get; set; }

        private string revision;
        public string Revision
        {
            get => revision;
            set
            {
                revision = value;
                RaisePropertyChanged("Revision");
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

            if (Filter != null)
            {
                e.Accepted = string.IsNullOrWhiteSpace(Filter) || Filter.Length == 0 || np.NumberPartId.ToLower().Contains(Filter.ToLower());
            }
            else
            {
                e.Accepted = string.IsNullOrWhiteSpace(Revision) || Revision.Length == 0 || np.Customer.CustomerName.Contains(Customer) && np.NumberPartRev.Contains(Revision);
            }
        }

        private void FilterCustomerRevision(object sender, FilterEventArgs e)
        {
            Numberpart np = (Numberpart)e.Item;

            e.Accepted = string.IsNullOrWhiteSpace(Revision) || Revision.Length == 0  || (np.Customer.CustomerName.Contains(Customer) && np.NumberPartRev.Contains(Revision));
        }

    }
}
