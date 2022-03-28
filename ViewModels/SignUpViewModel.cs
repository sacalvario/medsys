using ECN.Contracts.Services;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ECN.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private string _EmployeeID;
        public string EmployeeID
        {
            get => _EmployeeID;
            set
            {
                if (_EmployeeID != value)
                {
                    _EmployeeID = value;
                    RaisePropertyChanged("EmployeeID");
                }
            }
        }
        private string _Username;
        public string Username
        {
            get => _Username;
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                    RaisePropertyChanged("Username");
                }
            }
        }
        private string _Password;
        public string Password
        {
            get => _Password;
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }
        private string _PasswordConfirmation;
        public string PasswordConfimartion
        {
            get => _PasswordConfirmation;
            set
            {
                if (_PasswordConfirmation != value)
                {
                    _PasswordConfirmation = value;
                    RaisePropertyChanged("PasswordConfirmation");
                }
            }
        }

        private readonly INavigationService _navigationService;
        private readonly ILoginDataService _loginDataService;
        private readonly IWindowManagerService _windowManagerService;

        public SignUpViewModel(INavigationService navigationService, ILoginDataService loginDataService, IWindowManagerService windowManagerService)
        {
            _navigationService = navigationService;
            _loginDataService = loginDataService;
            _windowManagerService = windowManagerService;
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

        private string EncodePassword(string originalPassword)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = new UnicodeEncoding().GetBytes(originalPassword);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }


        private void SignUp()
        {
            if (EmployeeID != null && Username != null && Password != null && PasswordConfimartion != null)
            {
                if (_loginDataService.Exist(EmployeeID))
                {
                    if (_loginDataService.IsNotRegistered(EmployeeID))
                    {
                        if (Password == PasswordConfimartion)
                        {
                            string pass = EncodePassword(Password);
                            User user = new User()
                            {
                                EmployeeId = int.Parse(EmployeeID),
                                Username = Username,
                                Password = pass
                            };

                            try
                            {
                                if (_loginDataService.SaveUser(user))
                                {
                                    _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Has sido registrado exitosamente al sistema de ECN's de Electri-Cord.");
                                    _navigationService.GoBack();
                                    Clean();
                                }
                            }
                            catch (Exception ex)
                            {
                                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al registrar - " + ex.ToString());
                            }
                        }
                        else
                        {
                            _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Las contraseñas deben de coincidir.");
                        }

                    }
                    else
                    {
                        _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "El número de empleado ingresado ya esta registrado.");
                    }
                }
                else
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "El número de empleado ingresado no fue localizado, intentalo de nuevo.");
                }
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Llena todos los campos.");
            }
        }

        private void Clean()
        {
            Username = null;
            EmployeeID = null;
            Password = null;
            PasswordConfimartion = null;
        }
    }
}
