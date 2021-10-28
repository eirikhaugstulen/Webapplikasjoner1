
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webapplikasjoner1.DAL
{
    public interface IAvgangerRepository
    {
        Task<bool> Lagre(Avganger innAvganger);
      
        Task<List<Avganger>> HentAlle();

        Task<bool> Endre(Avganger endreAvgang);

        Task<bool> Slett(int id);
    }
}