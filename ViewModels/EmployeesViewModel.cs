
using ECN.Contracts.Services;
using ECN.Models;

using GalaSoft.MvvmLight;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace ECN.ViewModels
{
    public class EmployeesViewModel : ViewModelBase
    {
        private IEcnDataService _ecnDataService;
        public EmployeesViewModel(IEcnDataService ecnDataService)
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
    }
}
