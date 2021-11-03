using System.Collections.Generic;
using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public interface IStrekningRepository
    {
        Task<bool> Lagre(Strekning strekning);
        Task<bool> Endre(Strekning strekning);
        Task<bool> Slett(string id);
        Task<List<Strekninger>> HentAlle();
        Task<Strekninger> HentEn(string id);
    }
}