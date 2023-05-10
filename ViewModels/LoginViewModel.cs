﻿using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Contracts.Views;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ECN.ViewModels
{
    public class LoginViewModel : ViewModelBase, INavigationAware
    {
        private readonly ILoginDataService _loginService;
        private readonly IEcnDataService _ecnDataService;
        private readonly INavigationService _navigationService;
        private readonly IWindowManagerService _windowManagerService;
        private IShellWindow _shellWindow;
        private ILoginWindow _loginWindow;

        public LoginViewModel(ILoginDataService loginDataService, IEcnDataService ecnDataService, INavigationService navigationService, IWindowManagerService windowManagerService)
        {
            _loginService = loginDataService;
            _ecnDataService = ecnDataService;
            _navigationService = navigationService;
            _windowManagerService = windowManagerService;
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

        private RelayCommand _NavigateToSignUpCommand;
        public RelayCommand NavigateToSignUpCommand
        {
            get
            {
                if (_NavigateToSignUpCommand == null)
                {
                    _NavigateToSignUpCommand = new RelayCommand(NavigateToSignUp);
                }
                return _NavigateToSignUpCommand;
            }
        }

        private string EncodePassword(string originalPassword)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = new UnicodeEncoding().GetBytes(originalPassword);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }

        private async void CheckLogin()
        {
            if (Username != null && Password != null)
            {
                string pass = EncodePassword(Password);

                Usuario user = _loginService.IniciarSesion(Username, Password);

                await Task.CompletedTask;

                if (user != null)
                {
                    UserRecord.Username = user.NombreUsuario;
                    UserRecord.Nombre = user.Nombre;
                    UserRecord.Correo = user.Correo;
                    UserRecord.TipoUsuario = user.TipoUsuario;
                    UserRecord.User_ID = user.IdUsuario;
                    //UserRecord.Telefono = Convert.ToInt32(user.Telefono);


                    _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "¡Bienvenido!");

                    if (Application.Current.Windows.OfType<IShellWindow>().Count() == 0)
                    {
                        _shellWindow = SimpleIoc.Default.GetInstance<IShellWindow>(Guid.NewGuid().ToString());
                        _loginWindow.CloseWindow();
                        _navigationService.UnsubscribeNavigation();
                        _navigationService.Initialize(_shellWindow.GetNavigationFrame());
                        _shellWindow.ShowWindow();
                        _navigationService.NavigateTo(typeof(EcnViewModel).FullName);
                        await Task.CompletedTask;
                    }
                }
                else
                {

                    _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Nombre de usuario o contraseña incorrecto/a");
                }
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Ingresa el nombre de usuario y la contraseña");
            }

        }

        private void NavigateToSignUp()
        {
            _navigationService.NavigateTo(typeof(SignUpViewModel).FullName);
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is ILoginWindow loginWindow)
            {
                _loginWindow = loginWindow;
            }

        }

        public void OnNavigatedFrom()
        {
        }
    }
}
