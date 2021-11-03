using System.Collections.Generic;
using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public interface IBillettRepository
    {
        Task<bool> Lagre(Billett innBillett);
      
        Task<List<Billetter>> HentAlle();

        Task<Billetter> HentEn(int id);

        Task<bool> Slett(int id);
    }
}