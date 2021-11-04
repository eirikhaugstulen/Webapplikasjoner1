using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public interface IKundeRepository
    {
        Task<bool> Lagre(Kunde kunde);
        Task<bool> Slett(string id);
    }
}