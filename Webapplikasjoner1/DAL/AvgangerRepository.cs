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

        public AvgangerRepository(BillettKontekst db,ILogger<AvgangerRepository> log )
        {
            _db = db;
            _log = log;
            
    }


        public async Task<bool> Lagre(Avgang innAvgang)
        {
            try
            {
                var nyAvgangRad= new Avganger();
                nyAvgangRad.Id = innAvgang.Id;
             
                nyAvgangRad.Dato = innAvgang.Dato;
                nyAvgangRad.Klokkeslett = innAvgang.Klokkeslett;
                nyAvgangRad.Pris = innAvgang.Pris;


                var sjekkStrekning = await _db.Strekningene.FindAsync(innAvgang.Strekning);
                
                if (sjekkStrekning == null)
                {
                    _log.LogInformation("Fant ikke Strekning i database");
                    return false;
                }
                else
                {
                    nyAvgangRad.Strekning = sjekkStrekning;
                }
                _db.Avgangene.Add(nyAvgangRad);
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
                    Pris = b.Pris,
                    Strekning = b.Strekning

                }).ToListAsync();
                
                return alleAvgangene;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Avganger> HentEn(int id)
        {
            Avganger enAvgang = await _db.Avgangene.FindAsync(id);
            
            var hentetAvgang = new Avganger()
            {
                Id = enAvgang.Id,
                Dato = enAvgang.Dato,
                Klokkeslett = enAvgang.Klokkeslett,
                Pris = enAvgang.Pris,
                Strekning = enAvgang.Strekning
            };
            return hentetAvgang;
        }
        
        public async Task<bool> Endre (Avgang endreAvgang)
        {
            try
            {
                Avganger enAvgang = await _db.Avgangene.FindAsync(endreAvgang.Id);
                enAvgang.Id = endreAvgang.Id;
                enAvgang.Dato = endreAvgang.Dato;
                enAvgang.Pris = endreAvgang.Pris;
                enAvgang.Klokkeslett = endreAvgang.Klokkeslett;
                var endreStrekning = await _db.Strekningene.FindAsync(endreAvgang.Id);
                if (endreStrekning == null)
                {
                    _log.LogInformation("Fant ikke strekningen");
                    return false;
                }
                enAvgang.Strekning = endreStrekning;
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
                    _db.Avgangene.Remove(enAvgang);
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