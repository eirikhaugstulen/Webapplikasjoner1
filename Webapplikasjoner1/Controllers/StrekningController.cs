using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;
using Webapplikasjoner1.Validation;


namespace Webapplikasjoner1.Controllers
{
    public class StrekningController : ControllerBase
    {
        private readonly IStrekningRepository _db;
        private ILogger<StrekningController> _log;
        private const string _loggetInn = "loggetInn";
    
    
        public StrekningController(IStrekningRepository db, ILogger <StrekningController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> Lagre(Strekning innStrekning)
        {
            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            if (!Validering.StrekningValidering(innStrekning))
            {
                _log.LogInformation("Feil i validering av strekning");
                return BadRequest("Feil i validering av strekning");
            }
            
            bool returOk = await _db.Lagre(innStrekning);

            if (!returOk)
            {
                _log.LogInformation("Strekningen ble ikke lagret");
                return BadRequest("Strekningen ble ikke lagret");
            }

            return Ok("Strekning lagret");
        }

        public async Task<ActionResult> Endre(Strekning innStrekning)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            if (!Validering.StrekningValidering(innStrekning))
            {
                _log.LogInformation("Feil i validering av strekning");
                return BadRequest("Feil i validering av strekning");
            }
            
            bool returOk = await _db.Endre(innStrekning);
            if (!returOk)
            {
                _log.LogInformation("Strekningen ble ikke endret");
                return BadRequest("Strekningen ble ikke endret");
            }

            return Ok("Strekning endret");
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
                _log.LogInformation("Strekningen ble ikke slettet");
                return NotFound("Strekningen ble ikke slettet");
            }
            
            return Ok("Strekning slettet");
        }

        public async Task<ActionResult> HentAlle()
        {
            List<Strekninger> alleStrekninger = await _db.HentAlle();

            return Ok(alleStrekninger);
        }

        public async Task<ActionResult> HentEn(string id)
        {

            Strekninger enStrekning = await _db.HentEn(id);

            if (enStrekning == null)
            {
                _log.LogInformation("Fant ikke strekningen");
                return NotFound("Fant ikke strekningen");
            }

            return Ok(enStrekning);
        }
        
    }
}