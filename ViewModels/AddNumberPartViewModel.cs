using ECN.Contracts.Services;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class AddNumberPartViewModel : ViewModelBase
    {
        private readonly INumberPartsDataService _numberPartsDataService;
        private readonly IWindowManagerService _windowManagerService;

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

        private Visibility _UpdateNumberPartVisibility;
        public Visibility UpdateNumberPartVisibility
        {
            get => _UpdateNumberPartVisibility;
            set
            {
                if (_UpdateNumberPartVisibility != value)
                {
                    _UpdateNumberPartVisibility = value;
                    RaisePropertyChanged("UpdateNumberPartVisibility");
                }
            }
        }

        private Visibility _AddNumberPartVisibility;
        public Visibility AddNumberPartVisibility
        {
            get => _AddNumberPartVisibility;
            set
            {
                if (_AddNumberPartVisibility != value)
                {
                    _AddNumberPartVisibility = value;
                    RaisePropertyChanged("AddNumberPartVisibility");
                }
            }
        }

        private ICommand _UpdateNumberPartCommand;
        public ICommand UpdateNumberPartCommand
        {
            get
            {
                if (_UpdateNumberPartCommand == null)
                {
                    _UpdateNumberPartCommand = new RelayCommand(UpdateNumberPart);
                }
                return _UpdateNumberPartCommand;
            }
        }

        private ICommand _AddNumberPartCommand;
        public ICommand AddNumberPartCommand
        {
            get
            {
                if (_AddNumberPartCommand == null)
                {
                    _AddNumberPartCommand = new RelayCommand(AddNumberPart);
                }
                return _AddNumberPartCommand;
            }
        }

        private bool _IsEdition;
        public bool IsEdition
        {
            get => _IsEdition;
            set
            {
                if (_IsEdition != value)
                {
                    _IsEdition = value;
                    RaisePropertyChanged("IsEdition");
                }
            }
        }
        public AddNumberPartViewModel(Numberpart numberpart, INumberPartsDataService numberPartsDataService, IWindowManagerService windowManagerService)
        {
            _numberPartsDataService = numberPartsDataService;
            _windowManagerService = windowManagerService;
            NumberPart = numberpart;

            if (NumberPart == null)
            {
                UpdateNumberPartVisibility = Visibility.Collapsed;
                AddNumberPartVisibility = Visibility.Visible;
                NumberPart = new Numberpart()
                {
                    NumberPartType = 1
                };
            }
            else
            {
                UpdateNumberPartVisibility = Visibility.Visible;
                AddNumberPartVisibility = Visibility.Collapsed;
                IsEdition = true;
            }

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

        private void ResetData()
        {
            NumberPart = new Numberpart()
            {
                NumberPartType = 1
            };
        }

        private void UpdateNumberPart()
        {
            if (NumberPart.NumberPartId != null && NumberPart.NumberPartRev != null)
            {
                try
                {
                    if (_numberPartsDataService.UpdateNumberPart(NumberPart))
                    {
                        _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se actualizo la información del número de parte correctamente.");
                    }
                }
                catch (Exception ex)
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al editar número de parte - " + ex.ToString());
                }
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Llena todo los campos.");
            }
        }

        private void AddNumberPart()
        {
            if (NumberPart.NumberPartId != null && NumberPart.NumberPartRev != null && NumberPart.Customer != null)
            {
                try
                {
                    if (_numberPartsDataService.AddNumberPart(NumberPart))
                    {
                        _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se añadio el número de parte correctamente.");
                        ResetData();
                    }

                }
                catch (Exception ex)
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al añadir número de parte - " + ex.ToString());
                }
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Llena todo los campos.");
            }
        }

    }
}
