using System.Collections.Generic;
using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public interface ILokasjonReposity
    {
        Task<bool> RegistrerLokasjon(Lokasjon lokasjon);
        Task<bool> SlettLokasjon(int id);
        Task<List<Lokasjon>> HentAlle();
        Task<Lokasjon> HentEn(int id);
    }
}