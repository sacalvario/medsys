using ECN.Contracts.Services;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class AddCustomerViewModel : ViewModelBase
    {
        private readonly INumberPartsDataService _numberPartsDataService;
        private readonly IWindowManagerService _windowManagerService;

        public AddCustomerViewModel(INumberPartsDataService numberPartsDataService, IWindowManagerService windowManagerService)
        {
            _numberPartsDataService = numberPartsDataService;
            _windowManagerService = windowManagerService;
            Customer = new Customer();
        }

        private Customer _Customer;
        public Customer Customer
        {
            get => _Customer;
            set
            {
                if (_Customer != value)
                {
                    _Customer = value;
                    RaisePropertyChanged("Customer");
                }
            }
        }

        private ICommand _AddCustomerCommand;
        public ICommand AddCustomerCommand
        {
            get
            {
                if (_AddCustomerCommand == null)
                {
                    _AddCustomerCommand = new RelayCommand(AddCustomer);
                }
                return _AddCustomerCommand;
            }
        }

        private void AddCustomer()
        {
            if (Customer.CustomerName != null)
            {
                try
                {
                    if (_numberPartsDataService.AddCustomer(Customer))
                    {
                        _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se añadio el cliente correctamente.");
                        ResetData();
                    }

                }
                catch (Exception ex)
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al añadir cliente - " + ex.ToString());
                }
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "El campo se llena obligatoriamente.");
            }
        }

        private void ResetData()
        {
            Customer = new Customer();
        }
    }
}
