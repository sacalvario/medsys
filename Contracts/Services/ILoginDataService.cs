using ECN.Models;

namespace ECN.Contracts.Services
{
    public interface ILoginDataService
    {
        User Login(string username, string password);
    }
}
