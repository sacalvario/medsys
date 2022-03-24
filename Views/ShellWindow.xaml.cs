using System.Windows.Controls;

using ECN.Contracts.Views;
using GalaSoft.MvvmLight.Messaging;

namespace ECN.Views
{
    public partial class ShellWindow : IShellWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage msg)
        {
            if (msg.Notification == "CloseWindow")
            {
                CloseWindow();
            }

        }

        public Frame GetNavigationFrame()
            => shellFrame;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();
    }
}
