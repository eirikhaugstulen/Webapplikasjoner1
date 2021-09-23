using System.Collections.Generic;
using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public interface IBillettRepository
    {
        Task<bool> Lagre(Billett innBillett);
        /*
        Task<bool> LagreFler(Billett[] innBilletter);
       */
        Task<List<Billett>> HentAlle();

        Task<Billett> HentEn(int id);

        Task<bool> Endre(Billett endreBillett);

        Task<bool> Slett(int id);
    }
}