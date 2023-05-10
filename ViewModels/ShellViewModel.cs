using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using ECN.Contracts.Services;
using ECN.Contracts.Views;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

using ModernWpf.Controls;

namespace ECN.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private IEcnDataService _ecnDataService;
        private ILoginWindow _loginWindow;
        private NavigationViewItem _selectedMenuItem;
        private ICommand _menuItemInvokedCommand;
        private ICommand _signOutCommand;

        public string Name => UserRecord.Nombre;
        public string Type => UserRecord.TipoUsuario == "M" ? "Medico" : "Recepcionista";

        private bool _Holidays;
        public bool Holidays
        {
            get => _Holidays;
            set
            {
                if (_Holidays != value)
                {
                    _Holidays = value;
                    RaisePropertyChanged("Holidays");

                    if (_Holidays)
                    {
                        _ = _ecnDataService.SetHolidays(UserRecord.Employee);
                    }
                    else
                    {
                        _ = _ecnDataService.RemoveHolidays(UserRecord.Employee);
                    }
                }
            }
        }

        public NavigationViewItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => Set(ref _selectedMenuItem, value);
        }

        public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ??= new RelayCommand(OnMenuItemInvoked);
        public ICommand SignOutCommand => _signOutCommand ??= new RelayCommand(SignOut);


        private Visibility _ApprovedECNSVisibility = Visibility.Collapsed;
        public Visibility ApprovedECNSVisibility
        {
            get => _ApprovedECNSVisibility;
            set
            {
                if (_ApprovedECNSVisibility != value)
                {
                    _ApprovedECNSVisibility = value;
                    RaisePropertyChanged("ApprovedECNSVisibility");
                }
            }
        }

        public ShellViewModel(INavigationService navigationService, IEcnDataService ecnDataService)
        {
            _navigationService = navigationService;
            _ecnDataService = ecnDataService;
            //Holidays = Convert.ToBoolean(UserRecord.Employee.EmployeeHolidays);
            
            //if (UserRecord.Employee_ID == 3806)
            //{
            //    ApprovedECNSVisibility = Visibility.Visible;
            //}
        }

        private void OnMenuItemInvoked()
        {
            NavigateTo(SelectedMenuItem.SetTargetPageType());
        }

        private void NavigateTo(Type targetViewModel)
        {
            if (targetViewModel != null)
            {
                _navigationService.NavigateTo(targetViewModel.FullName);
            }
        }
        
        private async void SignOut()
        {
            if (Application.Current.Windows.OfType<ILoginWindow>().Count() == 0)
            {
                _navigationService.UnsubscribeNavigation();
                _loginWindow = SimpleIoc.Default.GetInstance<ILoginWindow>(Guid.NewGuid().ToString());
                _navigationService.Initialize(_loginWindow.GetNavigationFrame());
                Messenger.Default.Send(new NotificationMessage("CloseWindow"));
                ViewModelLocator.UnregisterShellViewModel();
                _loginWindow.ShowWindow();
                _navigationService.NavigateTo(typeof(LoginViewModel).FullName, _loginWindow);

                await System.Threading.Tasks.Task.CompletedTask;
            }
        }

    }
}
