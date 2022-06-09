using ECN.Contracts.Views;

namespace ECN.Views
{
    /// <summary>
    /// Lógica de interacción para AddNumberPart.xaml
    /// </summary>
    public partial class AddNumberPart : IAddNumberPartWindow
    {
        public AddNumberPart()
        {
            InitializeComponent();
        }

        public void CloseWindow() => Close();

        public void ShowWindow() => Show();
    }
}
