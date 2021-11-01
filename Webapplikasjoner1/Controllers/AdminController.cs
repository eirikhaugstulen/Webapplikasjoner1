using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;
using Webapplikasjoner1.Validation;
using Microsoft.AspNetCore.Http;

namespace Webapplikasjoner1.Controllers
{
    [Route("[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _db;
        private ILogger<AdminController> _log;
        private const string _loggetInn = "loggetInn";

        public AdminController(IAdminRepository db, ILogger<AdminController> log)
        {
            _db = db;
            _log = log;
        }

        //Legg inn ting her
        public async Task<ActionResult> LoggInn(Admin admin)
        {
            bool validerBrukernavn = Validering.GyldigBrukernavn(admin.Brukernavn);
            bool validerPassord = Validering.GyldigPassord(admin.Passord);

            if (validerBrukernavn && validerPassord)
            {
                bool returOK = await _db.LoggInn(admin);
                if (!returOK)
                {
                    _log.LogInformation("Kunne ikke logge inn admin " + admin.Brukernavn);
                    HttpContext.Session.SetString(_loggetInn,"");
                    return Ok(false);
                }
                _log.LogInformation("Bruker ble logget inn");
                HttpContext.Session.SetString(_loggetInn, "loggetInn");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn,"");
            _log.LogInformation("Admin ble logget ut");
        }

        public async Task<ActionResult> OpprettAdmin(Admin admin)
        {
            bool validerBrukernavn = Validering.GyldigBrukernavn(admin.Brukernavn);
            bool validerPassord = Validering.GyldigPassord(admin.Passord);

            if (validerBrukernavn && validerPassord)
            {
                bool returOK = await _db.OpprettAdmin(admin);
                if (!returOK)
                {
                    _log.LogInformation("Adminbrukeren ble ikke opprettet");
                    return BadRequest("Adminbrukeren ble ikke opprettet");
                }
                return Ok("Admin med brukernavn " + admin.Brukernavn + " ble opprettet");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }
    }
}
