
using ECN.Contracts.Views;

using System.Windows.Controls;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class ShellLogin : ILoginWindow
    {

        public ShellLogin()
        {
            InitializeComponent();
        }


        public void CloseWindow() => Close();

        public void ShowWindow() => Show();

        public Frame GetNavigationFrame() => loginFrame;
    }
}
