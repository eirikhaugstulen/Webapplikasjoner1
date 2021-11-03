using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public class BillettRepository : IBillettRepository
    {
        private readonly BillettKontekst _db;
        private ILogger<BillettRepository> _log;

        public BillettRepository(BillettKontekst db,ILogger<BillettRepository> log )
        {
            _db = db;
            _log = log;
            
    }


        public async Task<bool> Lagre(Billett innBillett)
        {
            try
            {
                var nyBillettRad = new Billetter();

                nyBillettRad.Fornavn = innBillett.Fornavn;
                nyBillettRad.Etternavn = innBillett.Etternavn;
                nyBillettRad.Retur = innBillett.Retur;
                nyBillettRad.Type = innBillett.Type;
                nyBillettRad.TotalPris = innBillett.TotalPris;
                nyBillettRad.Antall = innBillett.Antall;
                nyBillettRad.OrdreNummer = innBillett.OrdreNummer;
                
                var sjekkAvgang = await _db.Avgangene.FindAsync(innBillett.Avgang);
                if (sjekkAvgang == null)
                {
                    _log.LogInformation("Fant ikke Avgang i database");
                    return false;
                }
                else
                {
                    nyBillettRad.Avgang = sjekkAvgang;
                }
                _db.Billettene.Add(nyBillettRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<List<Billetter>> HentAlle()
        {
            try
            {
                List<Billetter> alleBillettene = await _db.Billettene.Select(b => new Billetter
                {
                    Id = b.Id,
                    Fornavn = b.Fornavn,
                    Etternavn = b.Etternavn,
                    Retur = b.Retur,
                    OrdreNummer = b.OrdreNummer,

                    Avgang = b.Avgang,
                    TotalPris = b.TotalPris,
                    Type = b.Type,
                    Antall =b.Antall,
                    

                }).ToListAsync();
                
                return alleBillettene;
            }
            catch
            {
                return null;
            }
        }


        public async Task<Billetter> HentEn(int id)
        {
            Billetter enBillett = await _db.Billettene.FindAsync(id);

            if (enBillett == null) //Returnerer null som gir en NotFound error i controlleren
            {
                return null;
            }
          
                 var hentetBillett = new Billetter()
                {
                    Id = enBillett.Id,
                    Fornavn = enBillett.Fornavn,
                    Etternavn = enBillett.Etternavn,
                    Retur = enBillett.Retur,
                    OrdreNummer = enBillett.OrdreNummer,
                    Avgang = enBillett.Avgang,
                    TotalPris = enBillett.TotalPris,
                    
                    Type = enBillett.Type,
                    Antall = enBillett.Antall,
                };
                 return hentetBillett;
            }
        
        public async Task<bool> Slett(int id)
            {
                try
                {
                    Billetter enBillett = await _db.Billettene.FindAsync(id);
                    _db.Billettene.Remove(enBillett);
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

   
