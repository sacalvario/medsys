using ECN.Contracts.Services;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace ECN.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly ILoginDataService _loginService;
        private readonly IEcnDataService _ecnDataService;

        public LoginViewModel(ILoginDataService loginDataService, IEcnDataService ecnDataService)
        {
            _loginService = loginDataService;
            _ecnDataService = ecnDataService;
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                RaisePropertyChanged("Username");
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                RaisePropertyChanged("Password");
            }
        }

        private RelayCommand _LoginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null)
                {
                    _LoginCommand = new RelayCommand(CheckLogin);
                }
                return _LoginCommand;
            }
        }

        private async void CheckLogin()
        {
            User user = _loginService.Login(Username, Password);

            if (user != null)
            {
                Employee emp = await _ecnDataService.GetEmployeeAsync(user.EmployeeId);
                user.Employee = emp;

                UserRecord.Employee = user.Employee;
                UserRecord.Username = user.Username;
                UserRecord.Employee_ID = user.EmployeeId;

                Messenger.Default.Send(new NotificationMessage("ValidLogin"));
            }
            else
            {

                _ = MessageBox.Show("Incorrect username or password entered. Please try again.");
            }

        }

    }
}
