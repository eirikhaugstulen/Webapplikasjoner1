using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;
using Webapplikasjoner1.Validation;

namespace Webapplikasjoner1.Controllers
{
    public class AvgangerController
    {
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
    public class AvgangerController : ControllerBase
    {
        private readonly IAvgangerRepository _db;
        private ILogger<global::Webapplikasjoner1.Controllers.AvgangerController> _log;

        public AvgangerController(IAvgangerRepository db ,ILogger<global::Webapplikasjoner1.Controllers.AvgangerController> log)
        {
            _db = db;
            _log = log;
        }
        
        public async Task<ActionResult> Lagre(Avganger innAvganger)
        {
            bool valideringOk = Validering.AvgangerValidering(innAvganger);

            if(valideringOk){
                
                bool returOk = await _db.Lagre(innAvganger);
                
                if (!returOk )
                {
                    _log.LogInformation("Avgangen ble ikke lagret");
                    return BadRequest("Avgangen ble ikke lagret");
                }
                return Ok("Avgangen lagret");
            }
            
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }
        
        public async Task<ActionResult> HentAlle()
        {
            List<Avganger> alleAvgangene = await _db.HentAlle();  

            return Ok(alleAvgangene);
        }
        
        public async Task<ActionResult> Endre(Avganger endreAvganger)
        {
            bool validering = Validering.AvgangValidering(endreAvganger);
            
            if (validering)
            {
                bool returOK = await _db.Endre(endreAvganger);
                if (!returOK)
                {
                    _log.LogInformation("Avgangen ble ikke endret");
                    return BadRequest("Avgangen ble ikke endret");
                }
                return Ok("Avgangen ble endret");  
            }
            
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }

        public async Task<ActionResult> Slett(int id)
        {
            bool returOk = await _db.Slett(id);

            if (!returOk)
            {
                _log.LogInformation("Avgangen ble ikke slettet");
                return NotFound("Avgangen ble ikke slettet");
            }
            
            return Ok("Avgang slettet");
        }
    }
}
        
    }
}