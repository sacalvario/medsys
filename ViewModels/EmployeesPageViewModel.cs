﻿using ECN.Contracts.Services;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class EmployeesPageViewModel : ViewModelBase
    {
        public readonly IEcnDataService _ecnDataService;
        public readonly IWindowManagerService _windowManagerService;
        public EmployeesPageViewModel(IEcnDataService ecnDataService, IWindowManagerService windowManagerService)
        {
            _ecnDataService = ecnDataService;
            _windowManagerService = windowManagerService;
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
                    _OpenEmployeeManageWindowCommand = new RelayCommand<Employee>(OpenEmployeeManageWindow);
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

        public void UpdateEmployees()
        {
            
        }

        private void OpenEmployeeManageWindow(Employee Employee)
        {
            Messenger.Default.Send(new NotificationMessage<Employee>(Employee, "ShowManageEmployeeWindow"));
        }
    }
}