using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;
using Webapplikasjoner1.Validation;

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
            bool valideringOk = Validering.BillettValidering(innBillett);

            if(valideringOk){
                
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
        