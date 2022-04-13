
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

        public EcnRecordsViewModel EcnRecordsViewModel
            => SimpleIoc.Default.GetInstance<EcnRecordsViewModel>();

        public ChecklistViewModel ChecklistViewModel
            => SimpleIoc.Default.GetInstance<ChecklistViewModel>();

        public LoginViewModel LoginViewModel
            => SimpleIoc.Default.GetInstance<LoginViewModel>();

        public EcnRegistrationViewModel EcnRegistrationViewModel
            => SimpleIoc.Default.GetInstance<EcnRegistrationViewModel>();

        public EcnSignedViewModel EcnSignedViewModel
            => SimpleIoc.Default.GetInstance<EcnSignedViewModel>();

        public ErrorViewModel ErrorViewModel
            => SimpleIoc.Default.GetInstance<ErrorViewModel>();

        public ApprovedViewModel ApprovedViewModel
            => SimpleIoc.Default.GetInstance<ApprovedViewModel>();

        public NumberPartHistoryViewModel NumberPartHistoryViewModel
            => SimpleIoc.Default.GetInstance<NumberPartHistoryViewModel>();

        public ReportViewModel ReportViewModel
            => SimpleIoc.Default.GetInstance<ReportViewModel>();

        public DashboardViewModel DashboardViewModel
            => SimpleIoc.Default.GetInstance<DashboardViewModel>();

        public ShellLoginViewModel ShellLoginViewModel
            => SimpleIoc.Default.GetInstance<ShellLoginViewModel>();

        public SignUpViewModel SignUpViewModel
            => SimpleIoc.Default.GetInstance<SignUpViewModel>();

        public ViewModelLocator()
        {
            // App Host
            SimpleIoc.Default.Register<IApplicationHostService, ApplicationHostService>();

            // Data Services
            SimpleIoc.Default.Register<IEcnDataService, EcnDataService>();
            SimpleIoc.Default.Register<INumberPartsDataService, NumberPartsDataService>();
            SimpleIoc.Default.Register<ILoginDataService, LoginDataService>();

            // Services
            SimpleIoc.Default.Register<IWindowManagerService, WindowManagerService>();
            SimpleIoc.Default.Register<IPageService, PageService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<IOpenFileService, OpenFileService>();
            SimpleIoc.Default.Register<IMailService, MailService>();

            // Window
            SimpleIoc.Default.Register<IShellWindow, ShellWindow>();
            SimpleIoc.Default.Register<ILoginWindow, ShellLogin>();
            SimpleIoc.Default.Register<IShellDialogWindow, ShellDialogWindow>();
            SimpleIoc.Default.Register<INumberPartsWindow, NumberParts>();
            SimpleIoc.Default.Register<IEmployeesWindow, Employees>();
            SimpleIoc.Default.Register<ShellViewModel>();
            SimpleIoc.Default.Register<ShellDialogViewModel>();
            SimpleIoc.Default.Register<ShellLoginViewModel>();
            SimpleIoc.Default.Register<NumberPartsViewModel>();
            SimpleIoc.Default.Register<EmployeesViewModel>();
            SimpleIoc.Default.Register<Report>();
            SimpleIoc.Default.Register<ReportViewModel>();
            SimpleIoc.Default.Register<Models.Ecn>();

            // Pages
            Register<HistoryViewModel, History>();
            Register<EcnViewModel, Views.Ecn>();
            Register<HistoryDetailsViewModel, HistoryDetails>();
            Register<EcnRecordsViewModel, EcnRecords>();
            Register<ChecklistViewModel, Checklist>();
            Register<EcnRegistrationViewModel, EcnRegistration>();
            Register<EcnSignedViewModel, EcnSigned>();
            Register<ErrorViewModel, Error>();
            Register<ApprovedViewModel, Approved>();
            Register<NumberPartHistoryViewModel, NumberPartHistory>();
            Register<DashboardViewModel, Dashboard>();
            Register<LoginViewModel, Login>();
            Register<SignUpViewModel, SignUp>();
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

        public static void UnregisterNumberPartViewModel()
        {
            SimpleIoc.Default.Unregister<NumberPartsViewModel>();
            SimpleIoc.Default.Register<NumberPartsViewModel>();
        }

        public static void UnregisterEmployeesViewModel()
        {
            SimpleIoc.Default.Unregister<EmployeesViewModel>();
            SimpleIoc.Default.Register<EmployeesViewModel>();
        }

        public static void UnregisterShellViewModel()
        {
            SimpleIoc.Default.Unregister<ShellViewModel>();
            SimpleIoc.Default.Register<ShellViewModel>();
        }
    }
}
