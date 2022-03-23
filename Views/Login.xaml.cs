using ECN.Contracts.Services;
using ECN.Contracts.Views;
using ECN.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : ILoginWindow
    {
        private readonly INavigationService _navigationService;
        private IShellWindow _shellWindow;
        private const string AutoHideScrollBarsKey = "AutoHideScrollBars";
        public Login(INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            Application.Current.Resources[AutoHideScrollBarsKey] = true;
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage msg)
        {
            if (msg.Notification == "ValidLogin")
            {
                if (App.Current.Windows.OfType<IShellWindow>().Count() == 0)
                {
                    // Default activation that navigates to the apps default page
                    _shellWindow = SimpleIoc.Default.GetInstance<IShellWindow>(Guid.NewGuid().ToString());
                    _navigationService.Initialize(_shellWindow.GetNavigationFrame());
                    _shellWindow.ShowWindow();
                    CloseWindow();
                    _navigationService.NavigateTo(typeof(EcnViewModel).FullName);
                }
            }
        }

        public void CloseWindow() => Close();

        public void ShowWindow() => Show();

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                {((LoginViewModel)DataContext).Password = ((PasswordBox)sender).Password; }
            }
        }
    }
}
