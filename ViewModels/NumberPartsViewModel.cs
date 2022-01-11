using ECN.Contracts.Services;
using ECN.Models;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            CvsNumberParts.Filter += ApplyFilter;
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
                    RaisePropertyChanged();
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

        private void ApplyFilter(object sender, FilterEventArgs e)
        {
            Numberpart np = (Numberpart)e.Item;

            e.Accepted = string.IsNullOrWhiteSpace(Filter) || Filter.Length == 0 || np.NumberPartId.ToLower().Contains(Filter.ToLower());
        }

    }
}
