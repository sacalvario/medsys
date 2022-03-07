using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class NumberPartHistoryViewModel : ViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IEcnDataService _historyDataService;
        private readonly INumberPartsDataService _numberPartsDataService;
        public NumberPartHistoryViewModel(INavigationService navigationService, IEcnDataService historyDataService, INumberPartsDataService numberPartsDataService)
        {
            _navigationService = navigationService;
            _historyDataService = historyDataService;
            _numberPartsDataService = numberPartsDataService;

            GetNumberPartHistory();
            CvsNumberPartHistory = new CollectionViewSource()
            {
                Source = NumberPartHistory
            };

            CvsNumberPartHistory.GroupDescriptions.Add(new PropertyGroupDescription("Ecn.Year"));
            CvsNumberPartHistory.GroupDescriptions.Add(new PropertyGroupDescription("Ecn.MonthName"));
            CvsNumberPartHistory.SortDescriptions.Add(new SortDescription("Ecn.Year", ListSortDirection.Descending));
            CvsNumberPartHistory.SortDescriptions.Add(new SortDescription("Ecn.Month", ListSortDirection.Descending));
            CvsNumberPartHistory.SortDescriptions.Add(new SortDescription("Ecn.Id", ListSortDirection.Descending));

            CvsNumberPartHistory.Filter += ApplyFilter;

        }

        private ObservableCollection<EcnNumberpart> _NumberPartHistory;
        public ObservableCollection<EcnNumberpart> NumberPartHistory
        {
            get => _NumberPartHistory;
            set
            {
                if (_NumberPartHistory != value)
                {
                    _NumberPartHistory = value;
                    RaisePropertyChanged("NumberPartHistory");
                }
            }
        }

        private CollectionViewSource _CvsNumberPartHistory;
        public CollectionViewSource CvsNumberPartHistory
        {
            get => _CvsNumberPartHistory;
            set
            {
                if (_CvsNumberPartHistory != value)
                {
                    _CvsNumberPartHistory = value;
                    RaisePropertyChanged("CvsNumberPartHistory");
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
                OnFilterChanged();
            }
        }

        private void OnFilterChanged()
        {
            CvsNumberPartHistory.View.Refresh();
        }

        private void ApplyFilter(object sender, FilterEventArgs e)
        {
            EcnNumberpart ecn = (EcnNumberpart)e.Item;

            e.Accepted = string.IsNullOrWhiteSpace(Filter) || Filter.Length == 0 || ecn.Product.NumberPartId.ToLower().Contains(Filter.ToLower());
        }

        private ICommand _navigateToDetailCommand;
        public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<Ecn>(NavigateToDetail);

        private void NavigateToDetail(Ecn ecn)
        {
            if (ecn != null)
            {
                _navigationService.NavigateTo(typeof(HistoryDetailsViewModel).FullName, ecn);
            }
        }

        private async void GetNumberPartHistory()
        {
            NumberPartHistory = new ObservableCollection<EcnNumberpart>();
            var data = await _numberPartsDataService.GetNumberPartHistoryAsync();

            foreach(var item in data)
            {
                item.Product = await _numberPartsDataService.GetNumberPartAsync(item.ProductId);
                item.Product.Customer = await _numberPartsDataService.GetCustomerAsync(item.Product.CustomerId);
                item.Ecn = await _historyDataService.GetEcnAsync(item.EcnId);

                NumberPartHistory.Add(item);
            }
        }

        public void OnNavigatedFrom()
        {
        }

        public void OnNavigatedTo(object parameter)
        {
            GetNumberPartHistory();
            //CvsNumberPartHistory.Source = NumberPartHistory;
        }
    }


}
