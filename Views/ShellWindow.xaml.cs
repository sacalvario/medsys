using System.Windows.Controls;

using ECN.Contracts.Views;


namespace ECN.Views
{
    public partial class ShellWindow : IShellWindow
    {
        public ShellWindow()
        {
            InitializeComponent();

        }

        public Frame GetNavigationFrame()
            => shellFrame;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();
    }
}
