using ECN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECN.Contracts.Services
{
    public interface INumberPartsDataService
    {
        Task<IEnumerable<Numberpart>> GetNumberPartsAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task<NumberpartType> GetNumberpartTypeAsync(int id);

    }
}
