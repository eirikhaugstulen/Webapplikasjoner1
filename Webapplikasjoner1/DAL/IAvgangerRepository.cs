
using System.Collections.Generic;
using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public interface IAvgangerRepository
    {
        Task<bool> Lagre(Avgang innAvgang);
      
        Task<List<Avganger>> HentAlle();
        Task<Avganger> HentEn(int id);

        Task<bool> Endre(Avgang endreAvgang);

        Task<bool> Slett(int id);
    }
}