using ECN.ViewModels;

using System.Windows;
using System.Windows.Controls;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                { ((LoginViewModel)DataContext).Password = ((PasswordBox)sender).Password; }
            }
        }
    }
}
