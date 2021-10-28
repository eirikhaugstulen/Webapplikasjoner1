using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;
using Webapplikasjoner1.Validation;
using ILogger = Serilog.ILogger;

namespace Webapplikasjoner1.Controllers
{
    public class StrekningController : ControllerBase
    {
        private readonly IStrekningRepository _db;
        private ILogger<StrekningController> _log;
    
    
        public StrekningController(IStrekningRepository db, ILogger <StrekningController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> LagreStrekning(Strekning innStrekning)
        {
            if (!Validering.StrekningValidering(innStrekning))
            {
                _log.LogInformation("Feil i validering av strekning");
                return BadRequest("Feil i validering av strekning");
            }
            
            bool returOk = await _db.LagreStrekning(innStrekning);

            if (!returOk)
            {
                _log.LogInformation("Strekningen ble ikke lagret");
                return BadRequest("Strekningen ble ikke lagret");
            }

            return Ok("Strekning lagret");
        }

        public async Task<ActionResult> EndreStrekning(Strekning innStrekning)
        {
            if (!Validering.StrekningValidering(innStrekning))
            {
                _log.LogInformation("Feil i validering av strekning");
                return BadRequest("Feil i validering av strekning");
            }
            
            bool returOk = await _db.EndreStrekning(innStrekning);
            if (!returOk)
            {
                _log.LogInformation("Strekningen ble ikke endret");
                return BadRequest("Strekningen ble ikke endret");
            }

            return Ok("Strekning endret");
        }

        public async Task<ActionResult> SlettStrekning(int id)
        {
            bool returOk = await _db.SlettStrekning(id);
            
            if (!returOk)
            {
                _log.LogInformation("Strekningen ble ikke slettet");
                return NotFound("Strekningen ble ikke slettet");
            }
            
            return Ok("Strekning slettet");
        }

        public async Task<ActionResult> HentAlleStrekninger()
        {
            List<Strekning> alleStrekninger = await _db.HentAlleStrekninger();

            return Ok(alleStrekninger);
        }

        public async Task<ActionResult> HentEnStrekning(int id)
        {
            Strekning enStrekning = await _db.HentEn(id);

            if (enStrekning == null)
            {
                _log.LogInformation("Fant ikke strekningen");
                return NotFound("Fant ikke strekningen");
            }

            return Ok(enStrekning);
        }
        
    }
}