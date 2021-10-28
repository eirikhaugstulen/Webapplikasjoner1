using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public interface IStrekningRepository
    {
        Task<bool> LagreStrekning(Strekning strekning);
        Task<bool> EndreStrekning(Strekning strekning);
        Task<bool> SlettStrekning(int id);
    }
}