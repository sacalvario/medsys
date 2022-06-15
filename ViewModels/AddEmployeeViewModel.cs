

using ECN.Contracts.Services;
using ECN.Models;

using GalaSoft.MvvmLight;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
                    RaisePropertyChanged();
                }
            }
        }

        public AddEmployeeViewModel(Employee employee, IEcnDataService ecnDataService)
        {

        }

        public AddEmployeeViewModel(IEcnDataService ecnDataService)
        {
            _ecnDataService = ecnDataService;

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
