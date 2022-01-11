using ECN.Contracts.Services;
using ECN.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECN.Services
{
    public class EcnDataService : IEcnDataService
    {
        private readonly ecnContext context = null;
        public EcnDataService()
        {
            context = new ecnContext();
        }
        public async Task<IEnumerable<Ecn>> GetHistoryAsync()
        {
            await Task.CompletedTask;
            return GetHistory();
        }

        private IEnumerable<Ecn> GetHistory()
        {
            return context.Ecns.Where(data => data.EmployeeId == UserRecord.Employee_ID).ToList();
        }


        public async Task<Changetype> GetChangeTypeAsync(int id)
        {
            await Task.CompletedTask;
            return GetChangeType(id);
        }
        private Changetype GetChangeType(int id)
        {
            return context.Changetypes.Find(id);
        }

        public async Task<Documenttype> GetDocumentTypeAsync(int id)
        {
            await Task.CompletedTask;
            return GetDocumentType(id);
        }

        private Documenttype GetDocumentType(int id)
        {
            return context.Documenttypes.Find(id);
        }

        public async Task<Status> GetStatusAsync(int id)
        {
            await Task.CompletedTask;
            return GetStatus(id);
        }

        private Status GetStatus(int id)
        {
            return context.Statuses.Find(id);
        }

        public async Task<IEnumerable<Changetype>> GetChangeTypesAsync()
        {
            await Task.CompletedTask;
            return GetChangeTypes();
        }

        private IEnumerable<Changetype> GetChangeTypes()
        {
            return context.Changetypes.ToList();
        }

        public async Task<IEnumerable<Documenttype>> GetDocumentTypesAsync()
        {
            await Task.CompletedTask;
            return GetDocumentTypes();
        }

        private IEnumerable<Documenttype> GetDocumentTypes()
        {
            return context.Documenttypes.ToList();
        }

        public async Task<IEnumerable<EcoType>> GetEcoTypesAsync()
        {
            await Task.CompletedTask;
            return GetEcoTypes();
        }

        private IEnumerable<EcoType> GetEcoTypes()
        {
            return context.EcoTypes.ToList();
        }

        public void SaveEcn(Ecn ecn)
        {
            if (ecn != null)
            {
                context.Ecns.Add(ecn);
                context.SaveChanges();
            }
        }

        public async Task<EcnEco> GetEcnEcoAsync(int id)
        {
            await Task.CompletedTask;
            return GetEcnEco(id);
        }

        private EcnEco GetEcnEco(int id)
        {
            EcnEco ecnEco = context.EcnEcos.First(i => i.IdEcn == id);
            ecnEco.EcoType = context.EcoTypes.Find(ecnEco.EcoTypeId);
            return ecnEco;
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            await Task.CompletedTask;
            return GetEmployee(id);
        }

        private Employee GetEmployee(int id)
        {
            Employee employee = context.Employees.Find(id);
            employee.Department = context.Departments.Find(employee.DepartmentId);
            return employee;
        }

        private IEnumerable<Employee> GetEmployees()
        {
            return context.Employees.ToList().Where(i => i.EmployeeId != UserRecord.Employee_ID);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            await Task.CompletedTask;
            return GetEmployees();
        }

        public async Task<Department> GetDepartmentAsync(int id)
        {
            await Task.CompletedTask;
            return GetDepartment(id);
        }

        private Department GetDepartment(int id)
        {
            return context.Departments.Find(id);
        }

        public async Task<ICollection<EcnAttachment>> GetAttachmentsAsync(int ecn)
        {
            await Task.CompletedTask;
            return GetAttachments(ecn);
        }

        private ICollection<EcnAttachment> GetAttachments(int ecn)
        {
            return context.EcnAttachments.Where(i => i.EcnId == ecn).ToList();
        }

        public async Task<Attachment> GetAttachmentAsync(int id)
        {
            await Task.CompletedTask;
            return GetAttachment(id);
        }

        private Attachment GetAttachment(int id)
        {
            return context.Attachments.Find(id);
        }

        public async Task<ICollection<EcnRevision>> GetRevisionsAsync(int ecn)
        {
            await Task.CompletedTask;
            return GetRevisions(ecn);
        }

        private ICollection<EcnRevision> GetRevisions(int ecn)
        {
            return context.EcnRevisions.Where(i => i.EcnId == ecn).ToList();
        }

        public async Task<IEnumerable<Ecn>> GetEcnRecordsAsync()
        {
            await Task.CompletedTask;
            return GetEcnRecords();
        }

        private IEnumerable<Ecn> GetEcnRecords()
        {
            return context.Ecns.ToList();
        }

        public async Task<IEnumerable<Ecn>> GetChecklistAsync()
        {
            await Task.CompletedTask;
            return GetChecklist();
        }

        private IEnumerable<Ecn> GetChecklist()
        {
            return context.Ecns.Where(i => i.EcnRevisions.Any(j => j.StatusId == 5 && j.EmployeeId == UserRecord.Employee_ID)).ToList();
        }

        private List<Employee> AMEF()
        {
            List<Employee> AMEF = new List<Employee>
            {
                context.Employees.Find(126),
                context.Employees.Find(137),
                context.Employees.Find(92),
                context.Employees.Find(8),
                context.Employees.Find(108),
                context.Employees.Find(39),
                context.Employees.Find(2246),
                context.Employees.Find(198),
                context.Employees.Find(119)
            };

            var data = AMEF;

            foreach (var item in data)
            {
                item.Department = context.Departments.Find(item.DepartmentId);
            }

            return AMEF;
        }

        private List<Employee> AMEFAlta()
        {
            List<Employee> AMEF = new List<Employee>
            {
                context.Employees.Find(117),
                context.Employees.Find(2246),
                context.Employees.Find(119)
            };

            var data = AMEF;

            foreach (var item in data)
            {
                item.Department = context.Departments.Find(item.DepartmentId);
            }

            return AMEF;
        }

        private List<Employee> ManualdeCalidad()
        {
            List<Employee> QualityManual = new List<Employee>
            {
                context.Employees.Find(119),
                context.Employees.Find(31)
            };

            var data = QualityManual;

            foreach (var item in data)
            {
                item.Department = context.Departments.Find(item.DepartmentId);
            }

            return QualityManual;
        }

        public List<Employee> GetAMEF()
        {
            return AMEF();
        }

        public List<Employee> GetAMEFAlta()
        {
            return AMEFAlta();
        }

        public List<Employee> GetManualdeCalidad()
        {
            return ManualdeCalidad();
        }

        public void SignEcn(Ecn ecn)
        {
            EcnRevision revision = ecn.EcnRevisions.FirstOrDefault(data => data.EmployeeId == UserRecord.Employee_ID);
            revision.StatusId = 4;
            revision.RevisionDate = System.DateTime.Today;
            revision.RevisionHour = System.DateTime.Now.TimeOfDay;
            revision.Notes = "Firmado";

            EcnRevision nextrevision = ecn.EcnRevisions.FirstOrDefault(data => data.RevisionSequence == revision.RevisionSequence + 1);
            if (nextrevision != null)
                nextrevision.StatusId = 5;

            context.SaveChanges();
        }
    }
}
