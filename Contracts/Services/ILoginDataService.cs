using ECN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECN.Contracts.Services
{
    public interface ILoginDataService
    {
        User Login(string username, string password);
    }
}
