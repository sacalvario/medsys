
using ECN.Contracts.Views;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para Employees.xaml
    /// </summary>
    public partial class Employees : IEmployeesWindow
    {
        public Employees()
        {
            InitializeComponent();
        }

        public void CloseWindow() => Close();


        public void ShowWindow() => Show();
    }
}
