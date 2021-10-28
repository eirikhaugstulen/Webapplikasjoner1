using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public class StrekningRepository
    {
        private readonly BillettKontekst _db;
        private ILogger<StrekningRepository> _log;

        public StrekningRepository(BillettKontekst db, ILogger<StrekningRepository> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<bool> LagreStrekning(Strekning innStrekning)
        {
            return false;
        }

        public async Task<bool> EndreStrekning(Strekning innStrekning)
        {
            return false;
        }

        public async Task<bool> SlettStrekning(int id)
        {
            return false;
        }
    }
}