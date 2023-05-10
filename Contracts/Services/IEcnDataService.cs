﻿using ECN.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECN.Contracts.Services
{
    public interface IEcnDataService
    {
        Task<IEnumerable<Ecn>> GetHistoryAsync();
        Task<IEnumerable<Cita>> GetCitasAsync();
        Task<IEnumerable<Cita>> GetCitasRealizadasAsync();
        Task<IEnumerable<Ecn>> GetEcnRecordsAsync();
        Task<IEnumerable<Ecn>> GetChecklistAsync();
        Task<IEnumerable<Cita>> GetCitasPendientesAsync();
        Task<IEnumerable<Ecn>> GetApprovedAsync();
        Task<Changetype> GetChangeTypeAsync(int id);
        Task<Diagnostico> GetDiagnosticoAsync(int id);
        Task<Documenttype> GetDocumentTypeAsync(int id);
        Task<Status> GetStatusAsync(int id);
        Task<Estado> GetEstadoAsync(int id);
        Task<Paciente> GetPacienteAsync(int id);
        Task<EcnEco> GetEcnEcoAsync(int id);
        Task<Ecn> GetEcnAsync(int id);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<IEnumerable<Paciente>> GetPacientesAsync();
        Task<IEnumerable<Usuario>> GetMedicosAsync();
        Task<Employee> GetEmployeeAsync(int id);
        Task<IEnumerable<Changetype>> GetChangeTypesAsync();
        Task<IEnumerable<Documenttype>> GetDocumentTypesAsync();
        Task<IEnumerable<EcoType>> GetEcoTypesAsync();
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentAsync(int id);
        Task<ICollection<EcnAttachment>> GetAttachmentsAsync(int ecn);
        Task<Attachment> GetAttachmentAsync(int id);
        Task<EcnRevision> GetCurrentSignatureAsync(int ecn);
        Task<Enfermedad> GetEnfermedadAsync(int ecn);
        Task<ICollection<EcnRevision>> GetRevisionsAsync(int ecn);
        Task<ICollection<DiagnosticoSintoma>> GetDiagnosticoSintomasAsync(int ecn);
        Task<ICollection<DiagnosticosTratamiento>> GeTratamientoAsync(int ecn);
        Task<ICollection<EcnDocumenttype>> GetDocumentsAsync(int ecn);
        int GetSignatureCount(int ecn);
        bool SaveEcn(Ecn ecn);
        bool SaveCita(Cita cita);
        bool SaveDiagnostico(Diagnostico diagnostico); 
        bool SignEcn(Ecn ecn, string notes);
        bool UpgradeEcn(Ecn ecn);
        bool ApproveEcn(Ecn ecn);
        Employee NextToSignEcn(Ecn ecn);
        Employee FindSigned(Ecn ecn);
        bool AddEmployee(Employee employee);
        bool UpgradeEmployee(Employee employee);
        bool RefuseEcn(Ecn ecn, string notes);
        bool CloseEcn(Ecn ecn, string notes);
        bool CancelEcn(Ecn ecn, string notes);
        void UpgradeAttachment(int attach, string filename, byte[] file);
        bool SetHolidays(Employee employee);
        bool RemoveHolidays(Employee employee);
        int GetClosedEcnCount();
        int GetOnHoldEcnCount();
        int GetApprovedEcnCount();

    }
}
