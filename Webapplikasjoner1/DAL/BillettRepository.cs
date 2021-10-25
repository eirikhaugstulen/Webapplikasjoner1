using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public class BillettRepository : IBillettRepository
    {
        private readonly BillettKontekst _db;

        public BillettRepository(BillettKontekst db)
        {
            _db = db;
        }


        public async Task<bool> Lagre(Billett innBillett)
        {
            try
            {
                var nyBillettRad = new Billetter();
                nyBillettRad.Id = innBillett.Id;
             
                nyBillettRad.Fornavn = innBillett.Fornavn;
                nyBillettRad.Etternavn = innBillett.Etternavn;
                nyBillettRad.Dato = innBillett.Dato;
                nyBillettRad.Retur = innBillett.Retur;
                nyBillettRad.ReturDato = innBillett.ReturDato;
                nyBillettRad.Pris = innBillett.Pris;

                var sjekkFraStrekning = await _db.Strekninger.FindAsync(innBillett.FraSted);
                if(sjekkFraStrekning == null)
                {
                    _log.logInformation("Fant ikke Strekning i database");
                    return false;
                }
                else
                {
                    nyBillettRad.FraSted = sjekkFraStrekning;
                }
                var sjekkTilStrekning = await _db.Strekninger.FindAsync(innBillett.TilSted);
                if (sjekkFraStrekning == null)
                {
                    _log.logInformation("Fant ikke Strekning i database");
                    return false;
                }
                else
                {
                    nyBillettRad.TilSted = sjekkTilStrekning;
                }


                _db.Billetter.Add(nyBillettRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<List<Billett>> HentAlle()
        {
            try
            {
                List<Billett> alleBillettene = await _db.Billetter.Select(b => new Billett
                {
                    Id = b.Id,
                    TilSted = b.TilSted,
                    FraSted = b.FraSted,
                    Fornavn = b.Fornavn,
                    Etternavn = b.Etternavn,
                    Dato = b.Dato,
                    Retur = b.Retur,
                    ReturDato = b.ReturDato,
                    Pris = b.Pris,
                }).ToListAsync();
                
                return alleBillettene;
            }
            catch
            {
                return null;
            }
        }


        public async Task<Billett> HentEn(int id)
        {
            Billett enBillett = await _db.Billetter.FindAsync(id);
            
                var hentetBillett = new Billett()
                {
                    Id = enBillett.Id,
                    TilSted = enBillett.TilSted,
                    FraSted = enBillett.FraSted,
                    Fornavn = enBillett.Fornavn,
                    Etternavn = enBillett.Etternavn,
                    Dato = enBillett.Dato,
                    Retur = enBillett.Retur,
                    ReturDato = enBillett.ReturDato,
                    Pris = enBillett.Pris,
                };
            return hentetBillett;
        }

        /*Brukes ikke nå, men beholder den for Oblig 2*/
        public async Task<bool> Endre (Billett endreBillett)
        {
            try
            {
                Billett enBillett = await _db.Billetter.FindAsync(endreBillett.Id);
                enBillett.Fornavn = endreBillett.Fornavn;
                enBillett.Etternavn = endreBillett.Etternavn;
                enBillett.TilSted = endreBillett.TilSted;
                enBillett.FraSted = endreBillett.FraSted;
                enBillett.Dato = endreBillett.Dato;
                enBillett.Retur = endreBillett.Retur;
                enBillett.ReturDato = endreBillett.ReturDato;
                enBillett.Pris = endreBillett.Pris;
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
                    Billett enBillett = await _db.Billetter.FindAsync(id);
                    _db.Billetter.Remove(enBillett);
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

   
