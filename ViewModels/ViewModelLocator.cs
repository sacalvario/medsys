using System.Windows.Controls;

using ECN.Contracts.Services;
using ECN.Contracts.Views;
using ECN.Models;
using ECN.Services;
using ECN.Views;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

using Microsoft.Extensions.Configuration;

namespace ECN.ViewModels
{
    public class ViewModelLocator
    {
        private IPageService PageService
            => SimpleIoc.Default.GetInstance<IPageService>();

        public ShellViewModel ShellViewModel
            => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public MainViewModel MainViewModel
            => SimpleIoc.Default.GetInstance<MainViewModel>();

        public HistoryViewModel HistoryViewModel
            => SimpleIoc.Default.GetInstance<HistoryViewModel>();

        public EcnViewModel EcnViewModel
            => SimpleIoc.Default.GetInstance<EcnViewModel>();

        public NumberPartsViewModel NumberPartsViewModel
            => SimpleIoc.Default.GetInstance<NumberPartsViewModel>();

        public HistoryDetailsViewModel HistoryDetailsViewModel
            => SimpleIoc.Default.GetInstance<HistoryDetailsViewModel>();

        public EmployeesViewModel EmployeesViewModel
            => SimpleIoc.Default.GetInstance<EmployeesViewModel>();

        public ViewModelLocator()
        {
            // App Host
            SimpleIoc.Default.Register<IApplicationHostService, ApplicationHostService>();

            // Core Services
            //SimpleIoc.Default.Register<IServiceProvider, ServiceProvider>();

            // Data Services
            SimpleIoc.Default.Register<IEcnDataService, EcnDataService>();
            SimpleIoc.Default.Register<INumberPartsDataService, NumberPartsDataService>();

            // Services
            SimpleIoc.Default.Register<IWindowManagerService, WindowManagerService>();
            SimpleIoc.Default.Register<IPageService, PageService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<IOpenFileService, OpenFileService>();

            // Window
            SimpleIoc.Default.Register<IShellWindow, ShellWindow>();
            SimpleIoc.Default.Register<ShellViewModel>();
            SimpleIoc.Default.Register<NumberParts>();
            SimpleIoc.Default.Register<NumberPartsViewModel>();
            SimpleIoc.Default.Register<Employees>();
            SimpleIoc.Default.Register<EmployeesViewModel>();

            // Pages
            Register<MainViewModel, MainPage>();
            Register<HistoryViewModel, History>();
            Register<EcnViewModel, Views.Ecn>();
            Register<HistoryDetailsViewModel, HistoryDetails>();
        }

        private void Register<VM, V>()
            where VM : ViewModelBase
            where V : Page
        {
            SimpleIoc.Default.Register<VM>();
            SimpleIoc.Default.Register<V>();
            PageService.Configure<VM, V>();
        }

        public void AddConfiguration(IConfiguration configuration)
        {
            var appConfig = configuration
                .GetSection(nameof(AppConfig))
                .Get<AppConfig>();

            SimpleIoc.Default.Register(() => configuration);
            SimpleIoc.Default.Register(() => appConfig);
        }

    }
}
