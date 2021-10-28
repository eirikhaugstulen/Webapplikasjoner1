using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.Controllers
{
    [Route("[controller]/[action]")]
    
    public class LokasjonController : ControllerBase
    {
        private readonly ILokasjonReposity _db;
        private ILogger<LokasjonController> _logger;

        public LokasjonController(ILokasjonReposity db, ILogger<LokasjonController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<ActionResult> RegistrerLokasjon(Lokasjon lokasjon)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("loggetInn")))
            {
                return Unauthorized();
            }
            
            bool ok = Validation.Validering.gyldigStedsnavn(lokasjon.Stedsnavn);

            if (ok)
            {
                bool returOK = await _db.RegistrerLokasjon(lokasjon);
                if (!returOK )
                {
                    _logger.LogInformation("Lokasjonen ble ikke lagret");
                    return BadRequest("Lokasjonen ble ikke lagret");
                }
                return Ok("Lokasjon lagret");
            }
            _logger.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }

        public async Task<ActionResult> SlettLokasjon(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("loggetInn")))
            {
                return Unauthorized();
            }
            
            bool returOK = await _db.SlettLokasjon(id);

            if (!returOK)
            {
                _logger.LogInformation("Lokasjonen ble ikke slettet");
                return NotFound("Lokasjonen ble ikke slettet");
            }

            return Ok("Lokasjonen ble slettet");
        }

        public async Task<ActionResult> HentAlle()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("loggetInn")))
            {
                return Unauthorized();
            }
            
            List<Lokasjon> alleLokasjoner = await _db.HentAlle();
            return Ok(alleLokasjoner);
        }

        public async Task<ActionResult> HentEn(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("loggetInn")))
            {
                return Unauthorized();
            }

            Lokasjon lokasjon = await _db.HentEn(id);
            
            if (lokasjon == null)
            {
                _logger.LogInformation("Fant ikke lokasjon");
                return NotFound("Fant ikke lokasjonen");
            }
            return Ok("Fant lokasjonen");
        }
    }
}