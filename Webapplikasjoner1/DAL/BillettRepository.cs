using Microsoft.EntityFrameworkCore;
using System;
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
                var nyBillettRad = new Billett();
                nyBillettRad.Id = innBillett.Id;
                nyBillettRad.TilSted = innBillett.TilSted;
                nyBillettRad.FraSted = innBillett.FraSted;
                nyBillettRad.Fornavn = innBillett.Fornavn;
                nyBillettRad.Etternavn = innBillett.Etternavn;
                nyBillettRad.Antall = innBillett.Antall;
                nyBillettRad.Dato = innBillett.Dato;
                _db.Billetter.Add(nyBillettRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /*
        public async Task<bool> LagreFler(Billett [] innBilletter)
        {
            try
            {
                foreach(Billett b in innBilletter)
                {
                    var nyBillettRad = new Billett();
                    nyBillettRad.Id = b.Id;
                    nyBillettRad.TilSted = b.TilSted;
                    nyBillettRad.FraSted = b.FraSted;
                    nyBillettRad.Fornavn = b.Fornavn;
                    nyBillettRad.Etternavn = b.Etternavn;
                    nyBillettRad.Antall = b.Antall;
                    nyBillettRad.Dato = b.Dato;
                    _db.Billetter.Add(nyBillettRad);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }*/


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
                    Antall = b.Antall,
                    Dato = b.Dato,
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
                    Antall = enBillett.Antall,
                    Dato = enBillett.Dato,
                };
                return hentetBillett;
              
        }

        public async Task<bool> Endre (Billett endreBillett)
        {
            try
            {
                Billett enBillett = await _db.Billetter.FindAsync(endreBillett.Id);
                enBillett.Fornavn = endreBillett.Fornavn;
                enBillett.Etternavn = endreBillett.Etternavn;
                enBillett.TilSted = endreBillett.TilSted;
                enBillett.FraSted = endreBillett.FraSted;
                enBillett.Antall = endreBillett.Antall;
                enBillett.Dato = endreBillett.Dato;
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
                    return true;  
                }
                catch
                {
                    return false;
                }
            }

        }
    }

   
