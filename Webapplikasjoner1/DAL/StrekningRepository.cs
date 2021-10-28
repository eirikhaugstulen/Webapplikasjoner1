﻿using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public class StrekningRepository
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
                nyStrekningRad.Id = innStrekning.Id;
                var sjekkFraSted = await _db.Strekningene.FindAsync(innStrekning.FraSted);
                if (sjekkFraSted == null)
                {
                    _log.LogInformation("Fant ikke lokasjon i database");
                    return false;
                }
                else
                {
                    nyStrekningRad.FraSted = sjekkFraSted.FraSted;
                }

                var sjekkTilSted = await _db.Strekningene.FindAsync(innStrekning.TilSted);
                if (sjekkTilSted == null)
                {
                    _log.LogInformation("Fant ikke lokasjon i database");
                    return false;
                }
                else
                {
                    nyStrekningRad.TilSted = sjekkFraSted.TilSted;
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
                var endreStrekning = await _db.Strekningene.FindAsync(innStrekning.Id);
                endreStrekning.FraSted.Id = innStrekning.FraSted;
                endreStrekning.TilSted.Id = innStrekning.TilSted;
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
            }

            return true;
        }

        public async Task<bool> SlettStrekning(int id)
        {
            return false;
        }
    }
}