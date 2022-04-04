
namespace ECN.Contracts.Services
{
    public interface IMailService
    {
        void SendEmail(string email, int id, string name);
        void SendSignEmail(string email, int id, string signedname, string generatorname);
        void SendRefuseECNEmail(string email, int id, string signedname, string generatorname);
        void SendRefuseECNToGeneratorEmail(string email, int id, string refusedname, string generatorname);
        void SendCloseECN(string email, int id, string generatorname);
        void SendCancelECN(string email, int id, string generatorname);
    }
}
