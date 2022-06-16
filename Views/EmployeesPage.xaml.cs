using ECN.Models;
using ECN.ViewModels;

using GalaSoft.MvvmLight.Messaging;

using System.Windows.Controls;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage<Employee>>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage<Employee> obj)
        {
            if (obj.Notification == "ShowManageEmployeeWindow")
            {
                var addemploye = new AddEmployee
                {
                    DataContext = new AddEmployeeViewModel(obj.Content, ((EmployeesPageViewModel)DataContext)._ecnDataService)
                };
                _ = addemploye.ShowDialog();
            }
        }

    }
}
