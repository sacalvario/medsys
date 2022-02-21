
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para Ecn.xaml
    /// </summary>
    public partial class Ecn : Page
    {
        public Ecn()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage msg)
        {
            if (msg.Notification == "ShowNumberParts")
            {
                var numberparts = new NumberParts();
                _ = numberparts.ShowDialog();
            }

            else if (msg.Notification == "ShowEmployees")
            {
                var employees = new Employees();
                _ = employees.ShowDialog();
            }
        }

    }
}
