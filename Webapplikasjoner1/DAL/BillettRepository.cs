using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapplikasjoner1.Models;
using Microsoft.EntityFrameworkCore;

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
                nyBillettRad.Strekning = innBillett.Strekning;
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


        public async Task<List<Billett>> HentAlle()
        {
            try
            {
                List<Billett> alleBillettene = await _db.Billetter.Select(b => new Billett
                {
                    Id = b.Id,
                    Strekning = b.Strekning,
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
                    Strekning = enBillett.Strekning,
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
                Biletter enBillett await _db.Biletter.FindAsync(endreBillett.id);
                enBillett.Fornavn = endreBillett.Fornavn
                enBillett.Etternavn = endreBillett.Etternavn
                enBillett.Strekning = endreBillett.Strekning
                enBillett.Antall = endreBillett.Antall
                enBillett.Dato = endreBillett.Dato
                
            }
            catch
            {

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

   
