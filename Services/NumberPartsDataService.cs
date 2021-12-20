using ECN.Contracts.Services;
using ECN.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECN.Services
{
    public class NumberPartsDataService : INumberPartsDataService
    {
        private readonly MyDbContext context = null;

        public NumberPartsDataService()
        {
            context = new MyDbContext();
        }

        public async Task<IEnumerable<Numberpart>> GetNumberPartsAsync()
        {
            await Task.CompletedTask;
            return GetNumberParts();
        }

        public IEnumerable<Numberpart> GetNumberParts()
        {
            return context.Numberparts.ToList();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            await Task.CompletedTask;
            return GetCustomer(id);
        }

        public Customer GetCustomer(int id)
        {
            return context.Customers.Find(id);
        }

        public async Task<NumberpartType> GetNumberpartTypeAsync(int id)
        {
            await Task.CompletedTask;
            return GetNumberPartType(id);
        }

        public NumberpartType GetNumberPartType(int id)
        {
            return context.NumberpartTypes.Find(id);
        }

        public async Task<Numberpart> GetNumberPartAsync(int id)
        {
            await Task.CompletedTask;
            return GetNumberPart(id);
        }

        public Numberpart GetNumberPart(int id)
        {
            return context.Numberparts.Find(id);
        }

        public ICollection<EcnNumberpart> GetNumberPartsEcn(int ecn)
        {
            return context.EcnNumberparts.Where(i => i.EcnId == ecn).ToList();
        }

        public async Task<ICollection<EcnNumberpart>> GetNumberPartsEcnsAsync(int ecn)
        {
            await Task.CompletedTask;
            return GetNumberPartsEcn(ecn);
        }
    }
}
