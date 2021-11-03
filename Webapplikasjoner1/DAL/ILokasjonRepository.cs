using System.Collections.Generic;
using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public interface ILokasjonRepository
    {
        Task<bool> RegistrerLokasjon(Lokasjon lokasjon);
        Task<bool> SlettLokasjon(string id);
        Task<List<Lokasjon>> HentAlle();
        Task<Lokasjon> HentEn(string id);
    }
}