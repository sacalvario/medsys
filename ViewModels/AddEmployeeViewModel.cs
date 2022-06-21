

using ECN.Contracts.Services;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        private readonly IEcnDataService _ecnDataService;
        private readonly IWindowManagerService _windowManagerService;

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

        private ICommand _UpgradeEmployeeCommand;
        public ICommand UpgradeEmployeeCommand
        {
            get
            {
                if (_UpgradeEmployeeCommand == null)
                {
                    _UpgradeEmployeeCommand = new RelayCommand(UpgradeEmployee);
                }
                return _UpgradeEmployeeCommand;
            }
        }

        private ICommand _AddEmployeeCommand;
        public ICommand AddEmployeeCommand
        {
            get
            {
                if (_AddEmployeeCommand == null)
                {
                    _AddEmployeeCommand = new RelayCommand(AddEmployee);
                }
                return _AddEmployeeCommand;
            }
        }

        public AddEmployeeViewModel(Employee employee, IEcnDataService ecnDataService, IWindowManagerService windowManagerService) 
        {
            _ecnDataService = ecnDataService;
            _windowManagerService = windowManagerService;
            Employee = employee;

            if (Employee == null)
            {
                UpdateEmployeeVisibility = Visibility.Collapsed;
                AddEmployeeVisibility = Visibility.Visible;
                Employee = new Employee();
            }
            else
            {
                UpdateEmployeeVisibility = Visibility.Visible;
                AddEmployeeVisibility = Visibility.Collapsed;
            }

            GetDepartments();
        }

        private async void GetDepartments()
        {
            Departments = new ObservableCollection<Department>();

            var data = await _ecnDataService.GetDepartmentsAsync();
            foreach (var item in data)
            {
                Departments.Add(item);
            }

        }

        private void UpgradeEmployee()
        {
            if (_ecnDataService.UpgradeEmployee(Employee))
            {
                _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se actualizo la información del empleado correctamente.");
            }
        }

        private void AddEmployee()
        {
            if (_ecnDataService.AddEmployee(Employee))
            {
                _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se añadio el empleado correctamente.");
            }
        }
    }
}
