

using ECN.Contracts.Services;
using ECN.Models;

using GalaSoft.MvvmLight;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace ECN.ViewModels
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        private readonly IEcnDataService _ecnDataService;
        private ObservableCollection<Department> _Departments;
        public ObservableCollection<Department> Departments
        {
            get => _Departments;
            set
            {
                if (_Departments != value)
                {
                    _Departments = value;
                    RaisePropertyChanged("Departments");
                }
            }
        }

        private Employee _Employee;
        public Employee Employee
        {
            get => _Employee;
            set
            {
                if (_Employee != value)
                {
                    _Employee = value;
                    RaisePropertyChanged("Employee");
                }
            }
        }

        private Visibility _UpdateEmployeeVisibility;
        public Visibility UpdateEmployeeVisibility
        {
            get => _UpdateEmployeeVisibility;
            set
            {
                if (_UpdateEmployeeVisibility != value)
                {
                    _UpdateEmployeeVisibility = value;
                    RaisePropertyChanged("UpdateEmployeeVisibility");
                }
            }
        }

        private Visibility _AddEmployeeVisibility;
        public Visibility AddEmployeeVisibility
        {
            get => _AddEmployeeVisibility;
            set
            {
                if (_AddEmployeeVisibility != value)
                {
                    _AddEmployeeVisibility = value;
                    RaisePropertyChanged("AddEmployeeVisibility");
                }
            }
        }

        public AddEmployeeViewModel(Employee employee, IEcnDataService ecnDataService) 
        {
            _ecnDataService = ecnDataService;
            Employee = employee;

            //if (Employee.EmployeeId.ToString() != null)
            //{
            //    UpdateEmployeeVisibility = Visibility.Visible;
            //    AddEmployeeVisibility = Visibility.Collapsed;
            //}

            GetDepartments();
        }

        public async void GetDepartments()
        {
            Departments = new ObservableCollection<Department>();

            var data = await _ecnDataService.GetDepartmentsAsync();
            foreach (var item in data)
            {
                Departments.Add(item);
            }

        }
    }
}
