﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                nyBillettRad.Id = innBillett.Id;
             
                nyBillettRad.Fornavn = innBillett.Fornavn;
                nyBillettRad.Etternavn = innBillett.Etternavn;
                nyBillettRad.Retur = innBillett.Retur;
                nyBillettRad.Type = innBillett.Type;
                nyBillettRad.TotalPris = innBillett.Pris;
                nyBillettRad.Antall = innBillett.Antall;

                
                    var sjekkReturDato = await _db.Avgangene.FindAsync(innBillett.ReturDato);
                    if (nyBillettRad.Retur == true)
                    {
                        if (sjekkReturDato == null)
                        {
                            _log.LogInformation("Fant ikke Avgang i database");
                            return false;
                        }
                        else
                        {
                            nyBillettRad.ReturDato = sjekkReturDato;
                        } }
                    else
                    {
                        nyBillettRad.ReturDato = null;
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
        
        public async Task<List<Billett>> HentAlle()
        {
            try
            {
                List<Billett> alleBillettene = await _db.Billettene.Select(b => new Billett
                {
                    Id = b.Id,
                    Fornavn = b.Fornavn,
                    Etternavn = b.Etternavn,
                    Retur = b.Retur,
                    ReturDato = b.ReturDato.Id,

                    Avgang = b.Avgang.Id,
                    Pris = b.TotalPris,
                    Type = b.Type,
                    Antall =b.Antall,
                    Dato =b.Avgang.Dato,
                    Klokkeslett =b.Avgang.Klokkeslett,

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
            Billetter enBillett = await _db.Billettene.FindAsync(id);

            if (enBillett.Retur)
            {
                 var hentetBillett = new Billett()
                {
                    Id = enBillett.Id,
                    Fornavn = enBillett.Fornavn,
                    Etternavn = enBillett.Etternavn,
                    Retur = enBillett.Retur,
                    ReturDato = enBillett.ReturDato.Id,
                    Avgang = enBillett.Avgang.Id,
                    Pris = enBillett.TotalPris,
                    Type = enBillett.Type,
                    Antall = enBillett.Antall,
                    Dato = enBillett.Avgang.Dato,
                    Klokkeslett = enBillett.Avgang.Klokkeslett,
                };
                 return hentetBillett;
            }
            else
            {
                var hentetBillett = new Billett()
                {
                    Id = enBillett.Id,
                    Fornavn = enBillett.Fornavn,
                    Etternavn = enBillett.Etternavn,
                    Retur = enBillett.Retur,
                    Avgang = enBillett.Avgang.Id,
                    Pris = enBillett.TotalPris,
                    Type = enBillett.Type,
                    Antall = enBillett.Antall,
                    Dato = enBillett.Avgang.Dato,
                    Klokkeslett = enBillett.Avgang.Klokkeslett,
                };
                return hentetBillett;
            }
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

   
