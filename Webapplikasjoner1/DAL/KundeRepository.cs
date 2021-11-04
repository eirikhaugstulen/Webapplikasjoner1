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

        public async Task<bool> Lagre(Kunde innKunde)
        {
            try
            {
                var nyKundeRad = new Kunder();
                
                nyKundeRad.KundeId = innKunde.KundeId;
                nyKundeRad.Fornavn = innKunde.Fornavn;
                nyKundeRad.Etternavn = innKunde.Etternavn;
                nyKundeRad.Adresse = innKunde.Adresse;
                nyKundeRad.Telefonnummer = innKunde.Telefonnummer;
                nyKundeRad.Epost = innKunde.Epost;
                
                _db.Kundene.Add(nyKundeRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _log.LogInformation("Kunne ikke registrere ny Kunde");
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
