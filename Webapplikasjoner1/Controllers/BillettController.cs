using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.Controllers
{
    [Route("[controller]/[action]")]
    public class BillettController : ControllerBase
    {
        private readonly IBillettRepository _db;
        private ILogger<BillettController> _log;

        public BillettController(IBillettRepository db ,ILogger<BillettController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> Lagre(Billett innBillett)
        {
            
            if (ModelState.IsValid)
            {
                bool returOk = await _db.Lagre(innBillett);
                if (!returOk )
                {
                    _log.LogInformation("Billetten ble ikke lagret");
                    return BadRequest("Billettten ble ikke lagret");
                }
                return Ok("Billett lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }

        public async Task<ActionResult> LagreFler(Billett [] billetter)
        {
           bool returOk = false;
            if (billetter.Length == 2)
            {
                foreach (Billett b in billetter)
                {
                    if (ModelState.IsValid)
                    {
                        returOk = await _db.Lagre(b);
                    }
                    else
                    {
                        returOk = false;
                    }
                }
            }

            if (!returOk)
            {
                _log.LogInformation("Billettene ble ikke lagret");
                return BadRequest("Billetttene ble ikke lagret");
            }

            return Ok("Billettene lagret");
        }

        public async Task<ActionResult> HentAlle()
        {
            List<Billett> alleBilletter = await _db.HentAlle();  

            return Ok(alleBilletter);
         
        }

        public async Task<ActionResult> HentEn(int id)
        {
            Billett enBillett = await _db.HentEn(id);
            if(enBillett == null)
            {
                _log.LogInformation("Fant ikke billetten");
                return NotFound("Fant ikke billetten");
            }
            return Ok(enBillett);
        }

        public async Task<ActionResult> Endre(Billett endreBillett)
        {
            bool returOK = false;
            if (ModelState.IsValid)
            {
                returOK = await _db.Endre(endreBillett);
            }
            if (!returOK)
            {
                _log.LogInformation("Billetten ble ikke endret");
                return BadRequest("Billettten ble ikke endret");
            }
            return Ok("Billett ble endret");  
        }

        public async Task<ActionResult> Slett(int id)
        {

            bool returOk = await _db.Slett(id);

            if (!returOk)
            {
                _log.LogInformation("Billetten ble ikke slettet");
                return NotFound("Billetten ble ikke slettet");
            }
            return Ok("Billett slettet");
        }
    }
}
        