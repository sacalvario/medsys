using ECN.Contracts.Services;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECN.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        public int EmployeeID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfimartion { get; set; }

        private readonly INavigationService _navigationService;
        private readonly ILoginDataService _loginDataService;

        public SignUpViewModel(INavigationService navigationService, ILoginDataService loginDataService)
        {
            _navigationService = navigationService;
            _loginDataService = loginDataService;
        }

        private RelayCommand _NavigateToLoginCommand;
        public RelayCommand NavigateToLoginCommand
        {
            get
            {
                if (_NavigateToLoginCommand == null)
                {
                    _NavigateToLoginCommand = new RelayCommand(NavigateToSignUp);
                }
                return _NavigateToLoginCommand;
            }
        }

        private RelayCommand _SignUpCommand;
        public RelayCommand SignUpCommand
        {
            get
            {
                if (_SignUpCommand == null)
                {
                    _SignUpCommand = new RelayCommand(SignUp);
                }
                return _SignUpCommand;
            }
        }

        private void NavigateToSignUp()
        {
            _navigationService.NavigateTo(typeof(LoginViewModel).FullName);
        }

        private void SignUp()
        {

        }
    }
}
