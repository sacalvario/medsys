using ECN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECN.Contracts.Services
{
    public interface INumberPartsDataService
    {
        Task<IEnumerable<Numberpart>> GetNumberPartsAsync();
        Task<Numberpart> GetNumberPartAsync(int id);
        Task<Customer> GetCustomerAsync(int id);
        Task<NumberpartType> GetNumberpartTypeAsync(int id);
        Task<ICollection<EcnNumberpart>> GetNumberPartsEcnsAsync(int ecn);

    }
}
