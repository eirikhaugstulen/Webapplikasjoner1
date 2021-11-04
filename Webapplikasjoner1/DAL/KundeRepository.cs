using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public class KundeRepository : IKundeRepository
    {
        private readonly BillettKontekst _db;
        private ILogger<KundeRepository> _log;

        public KundeRepository(BillettKontekst db,ILogger<KundeRepository> log )
        {
            _db = db;
            _log = log;
        }
        
        // Lagre, slett

        public async Task<bool> Lagre(Lokasjon lokasjon)
        {
            try
            {
                var nyLokasjonRad = new Lokasjoner();
                
                nyLokasjonRad.StedsNummer = lokasjon.StedsNummer;
                nyLokasjonRad.Stedsnavn = lokasjon.Stedsnavn;
                
                _db.Lokasjonene.Add(nyLokasjonRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _log.LogInformation("Kunne ikke registrere ny lokasjon");
                _log.LogInformation(e.Message);
                return false;
            }
        }
        
        public async Task<bool> Slett(string id)
        {
            try
            {
                Lokasjoner lokasjon = await _db.Lokasjonene.FindAsync(id);
                _db.Lokasjonene.Remove(lokasjon);
                await _db.SaveChangesAsync();
                return true;  
            }
            catch
            {
                return false;
            }
        }
        
    }
    }
