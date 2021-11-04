using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;

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

        public async Task<ActionResult> Lagre(Lokasjon lokasjon)
        {
            return Ok();
        }

        public async Task<ActionResult> Slett(string id)
        {

            return Ok();
        }

    }
}