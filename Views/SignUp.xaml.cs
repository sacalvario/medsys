using ECN.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                { ((SignUpViewModel)DataContext).Password = ((PasswordBox)sender).Password; }
            }
        }

        private void txtPassword_PasswordChanged1(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                { ((SignUpViewModel)DataContext).PasswordConfimartion = ((PasswordBox)sender).Password; }
            }
        }
    }
}
