using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Webapplikasjoner1.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class BillettController : ControllerBase
    {
        private readonly BillettKontekst;    
        
        public BillettController(BillettKontekst db){
            _db=db;
        }
    }

    public async Task<bool> Lagre(Billett innBillett)
    {
        Try{
            var nyBillettRad = new Billett();
            nyBilletRad.id= innBillett.id;
            nyBilletRad.strekning= innBillett.strekning;
            nyBilletRad.fornavn= innBillett.fornavn;
            nyBilletRad.etternavn= innBillett.etternavn;
            nyBilletRad.antall= innBillett.antall;
            nyBilletRad.dato= innBillett.dato;
        }
        catch
        {
            return false;
        }
    }
    
    public async Task<list<Billett>> HentAlle()
    {
        try {
            List<Billett> alleBillettene = await _db.Billetter.Select(b=> new Billett{
                id = b.id,
                strekning = b.strekning
                fornavn = b.fornavn,
                etternavn = b.etternavn,
                antall = b.antall,
                dato = b.dato
            }
            return alleBillettene;
        }
        catch{
            return null;
        }
    }
    public async Task<bool> Slett(int id)
    {
        try{
            Biletter enBillett = await _db.Biletter.FindAsync(id);
            _db.Biletter.Remove(enBillett);
            await _db.SaveChangesAsync();
            return true;
        }
        catch{
            return false;
        }
    }
    public async Task<Billett> HentEn(int id)
    {
        try{
            Biletter enBillett = await _db.Biletter.FindAsync(id);
            var hentetBillett = new Billett(){
                id = b.id,
                strekning = b.strekning
                fornavn = b.fornavn,
                etternavn = b.etternavn,
                antall = b.antall,
                dato = b.dato
            }
        }
    }
    public async Task<bool> Endre (Billett endreBillett)
    {
        try
        {
            Biletter enBillett await _db.Biletter.FindAsync(endreBillett.id);

            
        }
    }
}
