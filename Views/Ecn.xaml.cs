using GalaSoft.MvvmLight.Messaging;
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
                numberparts.ShowDialog();
            }

            else if (msg.Notification == "ShowEmployees")
            {
                var employees = new Employees();
                employees.ShowDialog();
            }
        }

    }
}
