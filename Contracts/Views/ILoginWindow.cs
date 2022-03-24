
using System.Windows.Controls;

namespace ECN.Contracts.Views
{
    public interface ILoginWindow
    {
        void ShowWindow();

        void CloseWindow();

        Frame GetNavigationFrame();
    }
}
