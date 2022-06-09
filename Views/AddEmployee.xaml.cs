using ECN.Contracts.Views;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : IAddEmployeeWindow
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        public void CloseWindow() => Close();

        public void ShowWindow() => Show();
    }
}
