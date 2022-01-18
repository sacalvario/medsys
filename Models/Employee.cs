using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

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
        public string Name => EmployeeFirstName + " " + EmployeeLastName;

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
