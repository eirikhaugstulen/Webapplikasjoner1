using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Validation;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;
namespace Webapplikasjoner1.Controllers
{
    
    [Microsoft.AspNetCore.Components.Route("[controller]/[action]")]
    public class AvgangerController : ControllerBase
    {
        private readonly IAvgangerRepository _db;
        private ILogger<AvgangerController> _log;
        private const string _loggetInn = "loggetInn";

        public AvgangerController(IAvgangerRepository db ,ILogger<AvgangerController> log)
        {
            _db = db;
            _log = log;
        }
        
        public async Task<ActionResult> Lagre(Avgang innAvganger)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }

            bool valideringOk = Validering.AvgangValidering(innAvganger);

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
        public async Task<ActionResult> HentEn(string id)
        {
            
            Avganger enAvgang = await _db.HentEn(id);
            
            if(enAvgang == null)
            {
                _log.LogInformation("Fant ikke avgangen");
                return NotFound("Fant ikke avgangen");
            }
            
            return Ok(enAvgang);
        }
        
        public async Task<ActionResult> Endre(Avgang endreAvganger)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            
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

        public async Task<ActionResult> Slett(string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            
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
        
    
