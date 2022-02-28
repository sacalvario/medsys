using System;
using System.Collections.Generic;
using System.Text;

namespace ECN.Contracts.Services
{
    public interface IMailService
    {
        void SendEmail(string email, int id, string name);
        void SendSignEmail(string email, int id, string signedname, string generatorname);
        void SendRefuseECNEmail(string email, int id, string signedname, string generatorname);
    }
}
