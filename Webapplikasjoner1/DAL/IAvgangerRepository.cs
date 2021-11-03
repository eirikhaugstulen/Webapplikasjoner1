
using System.Collections.Generic;
using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public interface IAvgangerRepository
    {
        Task<bool> Lagre(Avgang innAvgang);
      
        Task<List<Avganger>> HentAlle();
        Task<Avganger> HentEn(string id);

        Task<bool> Endre(Avgang endreAvgang);

        Task<bool> Slett(string id);
    }
}