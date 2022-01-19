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
                numberparts.Show();
            }

            else if (msg.Notification == "ShowEmployees")
            {
                var employees = new Employees();
                employees.Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tab.SelectedIndex = 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Tab.SelectedIndex = 2;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Tab.SelectedIndex = 3;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Tab.SelectedIndex = 4;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Tab.SelectedIndex = 0;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Tab.SelectedIndex = 1;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Tab.SelectedIndex = 2;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Tab.SelectedIndex = 3;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Tab.SelectedIndex = 5;
        }
    }
}
