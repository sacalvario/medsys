using System;
using System.Linq;
using System.Threading.Tasks;

using ECN.Contracts.Activation;
using ECN.Contracts.Services;
using ECN.Contracts.Views;
using ECN.ViewModels;

using GalaSoft.MvvmLight.Ioc;

namespace ECN.Services
{
    public class ApplicationHostService : IApplicationHostService
    {
        private readonly INavigationService _navigationService;
        private ILoginWindow _loginWindow;
        private bool _isInitialized;

        public ApplicationHostService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task StartAsync()
        {
            // Initialize services that you need before app activation
            await InitializeAsync();

            await HandleActivationAsync();

            // Tasks after activation
            await StartupAsync();
            _isInitialized = true;
        }

        public async Task StopAsync()
        {
            await Task.CompletedTask;
        }

        private async Task InitializeAsync()
        {
            if (!_isInitialized)
            {
                await Task.CompletedTask;
            }
        }

        private async Task StartupAsync()
        {
            if (!_isInitialized)
            {
                await Task.CompletedTask;
            }
        }

        private async Task HandleActivationAsync()
        {
            var activationHandler = SimpleIoc.Default.GetAllInstances<IActivationHandler>()
                                        .FirstOrDefault(h => h.CanHandle());

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync();
            }

            await Task.CompletedTask;

            if (System.Windows.Application.Current.Windows.OfType<ILoginWindow>().Count() == 0)
            {
                _loginWindow = SimpleIoc.Default.GetInstance<ILoginWindow>(Guid.NewGuid().ToString());
                _navigationService.Initialize(_loginWindow.GetNavigationFrame());
                _loginWindow.ShowWindow();
                _navigationService.NavigateTo(typeof(LoginViewModel).FullName, _loginWindow);

                await Task.CompletedTask;
            }
        }
    }
}
