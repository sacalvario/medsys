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
        }

        private void NotificationMessageReceived(NotificationMessage<Numberpart> obj)
        {
            if (obj.Notification == "ShowManageNumberPartWindow")
            {
                var addnumberpart = new AddNumberPart
                {
                    DataContext = new AddNumberPartViewModel(obj.Content, ((NumberPartsPageViewModel)DataContext)._numberPartsDataService)
                };
                _ = addnumberpart.ShowDialog();
            }
        }
    }
}
