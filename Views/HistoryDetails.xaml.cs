using ECN.ViewModels;

using GalaSoft.MvvmLight.Messaging;

using System.Windows.Controls;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para HistoryDetails.xaml
    /// </summary>
    public partial class HistoryDetails : Page
    {
        public HistoryDetails()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage<Models.Ecn>>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage<Models.Ecn> obj)
        {
            if (obj.Notification == "ShowReport")
            {
                var report = new Report
                {
                    DataContext = new ReportViewModel(obj.Content, ((HistoryDetailsViewModel)DataContext)._ecnDataService, ((HistoryDetailsViewModel)DataContext)._numberPartsDataService)
                };
                _ = report.ShowDialog();
            }
        }

    }
}
