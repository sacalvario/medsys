using ECN.Models;
using ECN.ViewModels;

using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para NumberPartsPage.xaml
    /// </summary>
    public partial class NumberPartsPage : Page
    {
        public NumberPartsPage()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage<Numberpart>>(this, NotificationMessageReceived);
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived2);
        }

        private void NotificationMessageReceived(NotificationMessage<Numberpart> obj)
        {
            if (obj.Notification == "ShowManageNumberPartWindow")
            {
                var addnumberpart = new AddNumberPart
                {
                    DataContext = new AddNumberPartViewModel(obj.Content, ((NumberPartsPageViewModel)DataContext)._numberPartsDataService, ((NumberPartsPageViewModel)DataContext)._windowManagerService)
                };
                _ = addnumberpart.ShowDialog();
            }
        }

        private void NotificationMessageReceived2(NotificationMessage obj)
        {
            if (obj.Notification == "ShowManageCustomerWindow")
            {
                var addcustomer = new AddCustomer
                {
                    DataContext = new AddCustomerViewModel(((NumberPartsPageViewModel)DataContext)._numberPartsDataService, ((NumberPartsPageViewModel)DataContext)._windowManagerService)
                };
                _ = addcustomer.ShowDialog();
            }
        }

    }
}
