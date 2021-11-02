using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.Models;
using ILogger = Serilog.ILogger;

namespace Webapplikasjoner1.DAL
{
    public class StrekningRepository : IStrekningRepository
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
            try
            {
                var nyStrekningRad = new Strekninger();

                nyStrekningRad.StrekningNummer = innStrekning.StrekningNummer;
                var sjekkFraSted = await _db.Lokasjonene.FindAsync(innStrekning.FraSted);
                if (sjekkFraSted == null)
                {
                    _log.LogInformation("Fant ikke lokasjon i database");
                    return false;
                }
                else
                {
                    nyStrekningRad.FraSted = sjekkFraSted;
                }

                var sjekkTilSted = await _db.Lokasjonene.FindAsync(innStrekning.TilSted);
                if (sjekkTilSted == null)
                {
                    _log.LogInformation("Fant ikke lokasjon i database");
                    return false;
                }
                else
                {
                    nyStrekningRad.TilSted = sjekkTilSted;
                }

                _db.Strekningene.Add(nyStrekningRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EndreStrekning(Strekning innStrekning)
        {
            try
            {
                var endreStrekning = await _db.Strekningene.FindAsync(innStrekning.StrekningNummer);

                
                    var sjekkFraSted =   _db.Lokasjonene.Find(innStrekning.FraSted);
                    if (sjekkFraSted == null)
                    {
                        _log.LogInformation("Fant ikke lokasjon i database");
                        return false;
                    }
                   
                        
                    
                
                
                
                    var sjekkTilSted =   _db.Lokasjonene.Find(innStrekning.TilSted);
                    if (sjekkTilSted == null)
                    {
                        _log.LogInformation("Fant ikke lokasjon i database");
                        return false;
                    }
                    
                    endreStrekning.FraSted.StedsNummer = innStrekning.FraSted;
                    endreStrekning.FraSted.StedsNummer = innStrekning.TilSted;
                    
                
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> SlettStrekning(string id)
        {
            try
            {
                Strekninger enStrekning = await _db.Strekningene.FindAsync(id);
                _db.Strekningene.Remove(enStrekning);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Strekning>> HentAlleStrekninger()
        { 
            try
            {
                List<Strekning> alleStrekninger = await _db.Strekningene.Select(s => new Strekning
                {
                    StrekningNummer = s.StrekningNummer,
                    FraSted = s.FraSted.StedsNummer,
                    TilSted = s.TilSted.StedsNummer,
                    
                }).ToListAsync();
                return alleStrekninger;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Strekning> HentEn(string id)
        {
            Strekninger enStrekning = await _db.Strekningene.FindAsync(id);

            var hentetStrekning = new Strekning()
            {
                StrekningNummer = enStrekning.StrekningNummer,
                FraSted = enStrekning.FraSted.StedsNummer,
                TilSted = enStrekning.TilSted.StedsNummer,
            }; 
            
            return hentetStrekning;
        }
    }
}