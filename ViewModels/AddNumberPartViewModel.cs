using ECN.Contracts.Services;
using ECN.Models;

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ECN.ViewModels
{
    public class AddNumberPartViewModel : ViewModelBase
    {
        private readonly INumberPartsDataService _numberPartsDataService;
        private ObservableCollection<Customer> _Customers;
        public ObservableCollection<Customer> Customers
        {
            get => _Customers;
            set
            {
                if (_Customers != value)
                {
                    _Customers = value;
                    RaisePropertyChanged("Customers");
                }
            }
        }

        private Numberpart _NumberPart;
        public Numberpart NumberPart
        {
            get => _NumberPart;
            set
            {
                if (_NumberPart != value)
                {
                    _NumberPart = value;
                    RaisePropertyChanged("NumberPart");
                }
            }
        }
        public AddNumberPartViewModel(Numberpart numberpart, INumberPartsDataService numberPartsDataService)
        {
            _numberPartsDataService = numberPartsDataService;
            NumberPart = numberpart;

            GetCustomers();
        }

        public async void GetCustomers()
        {
            Customers = new ObservableCollection<Customer>();

            var data = await _numberPartsDataService.GetCustomersAsync();
            foreach (var item in data)
            {
                Customers.Add(item);
            }

        }
    }
}
