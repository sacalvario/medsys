using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Contracts.Views;
using GalaSoft.MvvmLight.Ioc;
using MahApps.Metro.Controls;

namespace ECN.Services
{
    public class WindowManagerService : IWindowManagerService
    {
        private readonly IPageService _pageService;

        public Window MainWindow
            => Application.Current.MainWindow;

        public WindowManagerService (IPageService pageService)
        {
            _pageService = pageService;
        }

        public void OpenInNewWindow(string key, object parameter = null)
        {
            var window = GetWindow(key);
            if (window != null)
            {
                window.Show();
            }
            else
            {
                window = new MetroWindow()
                {
                    Title = "ECN",
                    Style = Application.Current.FindResource("CustomMetroWindow") as Style
                };
                var frame = new Frame()
                {
                    Focusable = false,
                    NavigationUIVisibility = NavigationUIVisibility.Hidden
                };

                window.Content = frame;
                var page = _pageService.GetPage(key);
                window.Closed += OnWindowClosed;
                window.Show();
                frame.Navigated += OnNavigated;
                var navigated = frame.Navigate(page, parameter);
            }
        }

        public bool? OpenInDialog(string key, object parameter)
        {
            var shellwindow = SimpleIoc.Default.GetInstance<IShellDialogWindow>(Guid.NewGuid().ToString()) as Window;
            var frame = ((IShellDialogWindow)shellwindow).GetDialogFrame();
            frame.Navigated += OnNavigated;
            shellwindow.Closed += OnWindowClosed;
            var page = _pageService.GetPage(key);
            var navigated = frame.Navigate(page, parameter);
            return shellwindow.ShowDialog();
        }

        public Window GetWindow(string key)
        {
            foreach (Window window in Application.Current.Windows)
            {
                var dataContext = window.GetDataContext();
                if (dataContext?.GetType().FullName == key)
                {
                    return window;
                }
            }

            return null;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (sender is Frame frame)
            {
                var dataContext = frame.GetDataContext();
                if (dataContext is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(e.ExtraData);
                }
            }
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            if (sender is Window window)
            {
                if (window.Content is Frame frame)
                {
                    frame.Navigated -= OnNavigated;
                }

                window.Closed -= OnWindowClosed;
            }
        }

    }
}
