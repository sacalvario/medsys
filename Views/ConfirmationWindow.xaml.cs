using ECN.Contracts.Views;
using ECN.ViewModels;
using MahApps.Metro.Controls;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : MetroWindow, IConfirmationWindow
    {
        public ConfirmationWindow(ConfirmationWindowViewModel viewModel)
        {
            InitializeComponent();
            viewModel.SetResult = OnSetResult;
            DataContext = viewModel;
        }


        public void CloseWindow() => Close();

        public void ShowWindow() => Show();

        private void OnSetResult(bool? result)
        {
            DialogResult = result;
            Close();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public bool Result()
        {
            throw new System.NotImplementedException();
        }
    }
}
