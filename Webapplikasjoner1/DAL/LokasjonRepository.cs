using Microsoft.Extensions.Logging;

namespace Webapplikasjoner1.DAL
{
    public class LokasjonRepository : ILokasjonReposity
    {
        private readonly BillettKontekst _db;
        private ILogger<LokasjonRepository> _log;

        public LokasjonRepository(BillettKontekst db,ILogger<LokasjonRepository> log )
        {
            _db = db;
            _log = log;
        }
    }
}