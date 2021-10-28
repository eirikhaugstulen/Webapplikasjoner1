using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
   
    public class AvgangerRepository : IAvgangerRepository
    {
        private readonly BillettKontekst _db;
        private ILogger<AvgangerRepository> _log;

        public AvgangerRepository(BillettKontekst db,ILogger<BillettRepository> log )
        {
            _db = db;
            _log = log;
            
    }


        public async Task<bool> Lagre(Avganger innAvgang)
        {
            try
            {
                var nyAvgangRad= new Avganger();
                nyAvgangRad.Id = innAvgang.Id;
             
                nyAvgangRad.Dato = innAvgang.Dato;
                nyAvgangRad.Klokkeslett = innAvgang.Klokkeslett;
                nyAvgangRad.Pris = innAvgang.Pris;
                nyAvgangRad.Strekning = innAvgang.Strekning;
                

                if (innAvgang.Retur == true)
                {
                    var sjekkReturDato = await _db.Avgangene.FindAsync(innBillett.ReturDato);
                    if (sjekkReturDato == null)
                    {
                        _log.LogInformation("Fant ikke Avgang i database");
                        return false;
                    }
                    else
                    {
                        nyBillettRad.ReturDato = sjekkReturDato;
                    }
                }
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
        
        public async Task<List<Avganger>> HentAlle()
        {
            try
            {
                List<Avganger> alleAvgangene = await _db.Avgangene.Select(b => new Avganger
                {
                    Id = b.Id,
                    Dato = b.Dato,
                    Klokkeslett = b.Klokkeslett,
                    Pris = b.Pris

                }).ToListAsync();
                
                return alleAvgangene;
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<bool> Endre (Avganger endreAvgang)
        {
            try
            {
                Avganger enAvgang = await _db.Avgangene.FindAsync(endreAvgang.Id);
                enAvgang.Id = endreAvgang.Id;
                enAvgang.Dato = endreAvgang.Dato;
                enAvgang.Pris = endreAvgang.Pris;
                enAvgang.Klokkeslett = endreAvgang.Klokkeslett;
                enAvgang.Strekning = endreAvgang.Strekning;
                await _db.SaveChangesAsync();
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<bool> Slett(int id)
            {
                try
                {
                    Avganger enAvgang = await _db.Avgangene.FindAsync(id);
                    _db.Billettene.Remove(enAvgang);
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