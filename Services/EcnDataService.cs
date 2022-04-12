
using ECN.Contracts.Services;
using ECN.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECN.Services
{
    public class EcnDataService : IEcnDataService
    {
        private readonly EcnContext context = null;
        public EcnDataService()
        {
            context = new EcnContext();
        }
        public async Task<IEnumerable<Ecn>> GetHistoryAsync()
        {
            await Task.CompletedTask;
            return GetHistory();
        }

        private IEnumerable<Ecn> GetHistory()
        {
            using EcnContext context = new EcnContext();
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
            //using ecnContext context = new ecnContext();
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

        public bool SaveEcn(Ecn ecn)
        {
            if (ecn != null)
            {
                context.Ecns.Add(ecn);

                var result = context.SaveChanges();
                return result > 0;
            }
            return false;
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
            using EcnContext context = new EcnContext();
            return context.EcnRevisions.Where(i => i.EcnId == ecn).ToList();
        }

        public async Task<IEnumerable<Ecn>> GetEcnRecordsAsync()
        {
            await Task.CompletedTask;
            return GetEcnRecords();
        }

        private IEnumerable<Ecn> GetEcnRecords()
        {
            using EcnContext context = new EcnContext();
            return context.Ecns.ToList();
        }

        public async Task<IEnumerable<Ecn>> GetChecklistAsync()
        {
            await Task.CompletedTask;
            return GetChecklist();
        }

        private IEnumerable<Ecn> GetChecklist()
        {
            using EcnContext context = new EcnContext();
            return context.Ecns.Where(i => i.EcnRevisions.Any(j => j.StatusId == 5 && j.EmployeeId == UserRecord.Employee_ID)).ToList();
        }

        public bool SignEcn(Ecn ecn, string notes)
        {
            EcnRevision revision = context.EcnRevisions.First(data => data.EmployeeId == UserRecord.Employee_ID && data.EcnId == ecn.Id);
            revision.StatusId = 4;
            revision.RevisionDate = System.DateTime.Now;
            revision.Notes = notes;

            EcnRevision nextrevision = context.EcnRevisions.FirstOrDefault(data => data.RevisionSequence == revision.RevisionSequence + 1 && data.EcnId == ecn.Id);

            if (nextrevision != null)
            {
                nextrevision.StatusId = 5;
            }

            var result = context.SaveChanges();
            return result > 0;
           
        }

        public async Task<ICollection<EcnDocumenttype>> GetDocumentsAsync(int ecn)
        {
            await Task.CompletedTask;
            return GetDocuments(ecn);
        }

        private ICollection<EcnDocumenttype> GetDocuments(int ecn)
        {
            return context.EcnDocumenttypes.Where(i => i.EcnId == ecn).ToList();
        }

        public bool RefuseEcn(Ecn ecn, string notes)
        {
            EcnRevision revision = context.EcnRevisions.FirstOrDefault(data => data.EmployeeId == UserRecord.Employee_ID && data.EcnId == ecn.Id);
            revision.StatusId = 6;
            revision.RevisionDate = System.DateTime.Now;
            revision.Notes = notes;

            ecn.StatusId = 1;

            var result = context.SaveChanges();
            return result > 0;
        }

        public Employee NextToSignEcn(Ecn ecn)
        {
            EcnRevision revision = context.EcnRevisions.FirstOrDefault(data => data.EmployeeId == UserRecord.Employee_ID && data.EcnId == ecn.Id);

            EcnRevision nextrevision = context.EcnRevisions.FirstOrDefault(data => data.RevisionSequence == revision.RevisionSequence + 1 && data.EcnId == ecn.Id);

            if (nextrevision != null && nextrevision.StatusId == 5)
            {
                return GetEmployee(nextrevision.EmployeeId);
            }
            return null;
        }

        public async Task<EcnRevision> GetCurrentSignatureAsync(int ecn)
        {
            await Task.CompletedTask;
            return GetRevision(ecn);
        }

        private EcnRevision GetRevision(int ecn)
        {
            return context.EcnRevisions.First(i => i.EcnId == ecn && i.StatusId == 5);
        }

        public int GetSignatureCount(int ecn)
        {
            return context.EcnRevisions.Count(i => i.EcnId == ecn);
        }

        public async Task<IEnumerable<Ecn>> GetApprovedAsync()
        {
            await Task.CompletedTask;
            return GetApproved();
        }

        private IEnumerable<Ecn> GetApproved()
        {
            return context.Ecns.Where(data => data.StatusId == 4).ToList();
        }

        public async Task<Ecn> GetEcnAsync(int id)
        {
            await Task.CompletedTask;
            return GetEcn(id);
        }

        private Ecn GetEcn(int id)
        {
            return context.Ecns.Find(id);
        }

        public int GetClosedEcnCount()
        {
            return context.Ecns.Where(data => data.StatusId == 3 && data.EmployeeId == UserRecord.Employee_ID).Count();
        }

        public int GetOnHoldEcnCount()
        {
            return context.Ecns.Where(data => data.StatusId == 1 && data.EmployeeId == UserRecord.Employee_ID).Count();
        }

        public int GetApprovedEcnCount()
        {
            return context.Ecns.Where(data => data.StatusId == 4 && data.EmployeeId == UserRecord.Employee_ID).Count();
        }


        public bool CloseEcn(Ecn ecn, string notes)
        {
            ecn.StatusId = 3;
            ecn.EndDate = System.DateTime.Today;
            ecn.Notes = notes;

            if (ecn.DocumentTypeId == 2 || ecn.DocumentTypeId == 4 || ecn.DocumentTypeId == 15 || ecn.DocumentTypeId == 16)
            {
                var data = context.EcnNumberparts.Where(data => data.EcnId == ecn.Id);
                foreach (var item in data)
                {
                    var context = new EcnContext();
                    item.Product = context.Numberparts.FirstOrDefault(data => data.NumberPartNo == item.ProductId);
                    item.Product.NumberPartRev = ecn.DrawingLvl;
                }
            }

            var result = context.SaveChanges();
            return result > 0;
        }

        public bool CancelEcn(Ecn ecn, string notes)
        {
            ecn.StatusId = 2;
            ecn.EndDate = System.DateTime.Today;
            ecn.Notes = notes;

            var result = context.SaveChanges();
            return result > 0;
        }

        public void UpgradeAttachment(int attach, string filename, byte[] file)
        {
            Attachment upgraded = context.Attachments.Find(attach);
            upgraded.AttachmentFilename = filename;
            upgraded.AttachmentFile = file;
        }

        public bool UpgradeEcn(Ecn ecn)
        {
            EcnRevision refusedrevision = context.EcnRevisions.OrderBy(data => data.RevisionSequence).Last(data => data.StatusId == 6);
            refusedrevision.StatusId = 5;

            Ecn upgradedecn = context.Ecns.Find(ecn.Id);
            upgradedecn.StatusId = 5;

            var result = context.SaveChanges();
            return result > 0;
        }

        public Employee FindSigned(Ecn ecn)
        {
            EcnRevision revision = context.EcnRevisions.FirstOrDefault(data => data.StatusId == 5 && data.EcnId == ecn.Id);

            if (revision != null)
            {
                return GetEmployee(revision.EmployeeId);
            }
            return null;
        }

        public bool ApproveEcn(Ecn ecn)
        {
            ecn.StatusId = 4;

            var result = context.SaveChanges();
            return result > 0;
        }
    }
}
