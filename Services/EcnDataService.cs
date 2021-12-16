using ECN.Contracts.Services;
using ECN.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECN.Services
{
    public class EcnDataService : IEcnDataService
    {
        private readonly MyDbContext context = null;
        public EcnDataService()
        {
            context = new MyDbContext();
        }
        public async Task<IEnumerable<Ecn>> GetHistoryAsync()
        {
            await Task.CompletedTask;
            return GetHistory();
        }

        public IEnumerable<Ecn> GetHistory()
        {
            return context.Ecns.Where(data => data.EmployeeId == 212).ToList();
        }


        public async Task<Changetype> GetChangeTypeAsync(int id)
        {
            await Task.CompletedTask;
            return GetChangeType(id);
        }
        public Changetype GetChangeType(int id)
        {
            return context.Changetypes.Find(id);
        }

        public async Task<Documenttype> GetDocumentTypeAsync(int id)
        {
            await Task.CompletedTask;
            return GetDocumentType(id);
        }

        public Documenttype GetDocumentType(int id)
        {
            return context.Documenttypes.Find(id);
        }

        public async Task<Status> GetStatusAsync(int id)
        {
            await Task.CompletedTask;
            return GetStatus(id);
        }

        public Status GetStatus(int id)
        {
            return context.Statuses.Find(id);
        }

        public async Task<IEnumerable<Changetype>> GetChangeTypesAsync()
        {
            await Task.CompletedTask;
            return GetChangeTypes();
        }

        public IEnumerable<Changetype> GetChangeTypes()
        {
            return context.Changetypes.ToList();
        }

        public async Task<IEnumerable<Documenttype>> GetDocumentTypesAsync()
        {
            await Task.CompletedTask;
            return GetDocumentTypes();
        }

        public IEnumerable<Documenttype> GetDocumentTypes()
        {
            return context.Documenttypes.ToList();
        }

        public async Task<IEnumerable<EcoType>> GetEcoTypesAsync()
        {
            await Task.CompletedTask;
            return GetEcoTypes();
        }

        public IEnumerable<EcoType> GetEcoTypes()
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

        public EcnEco GetEcnEco(int id)
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

        public Employee GetEmployee(int id)
        {
            Employee employee = context.Employees.Find(id);
            employee.Department = context.Departments.Find(employee.DepartmentId);
            return employee;
        }
    }
}
