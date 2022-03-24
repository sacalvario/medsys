
using ECN.Contracts.Services;
using ECN.Models;
using System.Linq;

namespace ECN.Services
{
    public class LoginDataService : ILoginDataService
    {
        private readonly ecnContext context = null;
        public LoginDataService()
        {
            context = new ecnContext();
        }
        public User Login(string username, string password)
        {
            if (username.All(char.IsDigit))
            {
                int employeenumber = int.Parse(username);
                return context.Users.FirstOrDefault(i => i.EmployeeId == employeenumber && i.Password == password);
            }
            else
            {
                return context.Users.FirstOrDefault(i => i.Username == username && i.Password == password);
            }
        }
    }
}
