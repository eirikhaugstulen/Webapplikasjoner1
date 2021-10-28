using System.Collections.Generic;
using System.Threading.Tasks;
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
            List<Lokasjon> alleLokasjoner = await _db.HentAlle();
            return Ok(alleLokasjoner);
        }
    }
}