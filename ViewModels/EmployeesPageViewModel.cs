using ECN.Contracts.Services;
using ECN.Contracts.Views;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class EmployeesPageViewModel : ViewModelBase
    {
        private readonly IEcnDataService _ecnDataService;
        private IAddEmployeeWindow _employeesWindow;
        public EmployeesPageViewModel(IEcnDataService ecnDataService)
        {
            _ecnDataService = ecnDataService;
            Employees = new ObservableCollection<Employee>();
            GetEmployees();

            CvsEmployees = new CollectionViewSource
            {
                Source = Employees
            };

            CvsEmployees.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));


            CvsEmployees.Filter += ApplyFilter;
        }

        private ICommand _OpenEmployeeManageWindowCommand;
        public ICommand OpenEmployeeManageWindowCommand
        {
            get
            {
                if (_OpenEmployeeManageWindowCommand == null)
                {
                    _OpenEmployeeManageWindowCommand = new RelayCommand(OpenEmployeeManageWindow);
                }
                return _OpenEmployeeManageWindowCommand;
            }
        }


        private ObservableCollection<Employee> _Employees;
        public ObservableCollection<Employee> Employees
        {
            get => _Employees;
            set
            {
                if (_Employees != value)
                {
                    _Employees = value;
                    RaisePropertyChanged("Employees");
                }
            }
        }

        internal CollectionViewSource CvsEmployees { get; set; }
        public ICollectionView EmployeeCollection => CvsEmployees.View;

        private string filter;
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                OnFilterChanged();
            }

        }
        private void OnFilterChanged()
        {
            CvsEmployees.View.Refresh();
        }

        private void ApplyFilter(object sender, FilterEventArgs e)
        {
            Employee er = (Employee)e.Item;

            e.Accepted = string.IsNullOrWhiteSpace(Filter) || Filter.Length == 0 || er.Name.ToLower().Contains(Filter.ToLower());
        }

        private async void GetEmployees()
        {
            var data = await _ecnDataService.GetEmployeesAsync();

            foreach (var item in data)
            {
                item.Department = await _ecnDataService.GetDepartmentAsync(item.DepartmentId);
                Employees.Add(item);
            }
        }

        private void OpenEmployeeManageWindow()
        {
            if (Application.Current.Windows.OfType<IAddEmployeeWindow>().Count() == 0)
            {
                _employeesWindow = SimpleIoc.Default.GetInstance<IAddEmployeeWindow>(Guid.NewGuid().ToString());
                _employeesWindow.ShowWindow();
            }
        }
    }
}
