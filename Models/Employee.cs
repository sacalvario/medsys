using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows;

#nullable disable

namespace ECN.Models
{
    public partial class Employee : ViewModelBase
    {
        public Employee()
        {
            EcnRevisions = new HashSet<EcnRevision>();
            Ecns = new HashSet<Ecn>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeEmail { get; set; }
        public int DepartmentId { get; set; }
        public sbyte EmployeeHolidays { get; set; }
        public string Name => EmployeeFirstName + " " + EmployeeLastName;

        private Visibility _IsInHolidaysVisibility = Visibility.Collapsed;
        public Visibility IsInHolidaysVisibility
        {
            get
            {
                if (Convert.ToBoolean(EmployeeHolidays))
                {
                    _IsInHolidaysVisibility = Visibility.Visible;
                }
                else
                {
                    _IsInHolidaysVisibility = Visibility.Collapsed;
                }

                return _IsInHolidaysVisibility;
            }
        }

        private int _Index;
        public int Index
        {
            get => _Index;
            set
            {
                if (_Index != value)
                {
                    _Index = value;
                    RaisePropertyChanged("Index");
                }
            }
        }

        public virtual Department Department { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<EcnRevision> EcnRevisions { get; set; }
        public virtual ICollection<Ecn> Ecns { get; set; }
    }
}
