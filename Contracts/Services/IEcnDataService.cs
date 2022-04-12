using ECN.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECN.Contracts.Services
{
    public interface IEcnDataService
    {
        Task<IEnumerable<Ecn>> GetHistoryAsync();
        Task<IEnumerable<Ecn>> GetEcnRecordsAsync();
        Task<IEnumerable<Ecn>> GetChecklistAsync();
        Task<IEnumerable<Ecn>> GetApprovedAsync();
        Task<Changetype> GetChangeTypeAsync(int id);
        Task<Documenttype> GetDocumentTypeAsync(int id);
        Task<Status> GetStatusAsync(int id);
        Task<EcnEco> GetEcnEcoAsync(int id);
        Task<Ecn> GetEcnAsync(int id);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeAsync(int id);
        Task<IEnumerable<Changetype>> GetChangeTypesAsync();
        Task<IEnumerable<Documenttype>> GetDocumentTypesAsync();
        Task<IEnumerable<EcoType>> GetEcoTypesAsync();
        Task<Department> GetDepartmentAsync(int id);
        Task<ICollection<EcnAttachment>> GetAttachmentsAsync(int ecn);
        Task<Attachment> GetAttachmentAsync(int id);
        Task<EcnRevision> GetCurrentSignatureAsync(int ecn);
        Task<ICollection<EcnRevision>> GetRevisionsAsync(int ecn);
        Task<ICollection<EcnDocumenttype>> GetDocumentsAsync(int ecn);
        int GetSignatureCount(int ecn);
        bool SaveEcn(Ecn ecn);
        bool SignEcn(Ecn ecn, string notes);
        bool UpgradeEcn(Ecn ecn);
        bool ApproveEcn(Ecn ecn);
        Employee NextToSignEcn(Ecn ecn);
        Employee FindSigned(Ecn ecn);
        bool RefuseEcn(Ecn ecn, string notes);
        bool CloseEcn(Ecn ecn, string notes);
        bool CancelEcn(Ecn ecn, string notes);
        void UpgradeAttachment(int attach, string filename, byte[] file);
        int GetClosedEcnCount();
        int GetOnHoldEcnCount();
        int GetApprovedEcnCount();
    }
}
