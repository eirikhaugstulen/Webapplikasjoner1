using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;
using Webapplikasjoner1.Validation;

namespace Webapplikasjoner1.Controllers
{
     [Route("[controller]/[action]")]
    
    public class KundeController : ControllerBase
    {
        private readonly IKundeRepository _db;
        private ILogger<KundeController> _logger;
        private const string _loggetInn = "loggetInn";

        public KundeController(IKundeRepository db, ILogger<KundeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<ActionResult> Lagre(Kunde kunde)
        {
            if (!Validering.KundeValidering(kunde))
            {
                _logger.LogInformation("Feil i inputvalidering");
                return BadRequest("Feil i inputvalidering");
            }
            
            bool returOK = await _db.Lagre(kunde);
            if (!returOK )
            {
                _logger.LogInformation("Kunde ble ikke lagret");
                return BadRequest("Kunde ble ikke lagret");
            }
            return Ok("Kunde lagret");
        }

        public async Task<ActionResult> Slett(string id)
        {
            bool returOK = await _db.Slett(id);

            if (!returOK)
            {
                _logger.LogInformation("Kunde ble ikke slettet");
                return NotFound("Kunde ble ikke slettet");
            }

            return Ok("Kunde ble slettet");
        }

    }
}