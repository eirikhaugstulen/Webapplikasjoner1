using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class BillettController : ControllerBase
    {
        public readonly IBillettRepository _db;

        public BillettController(IBillettRepository db)
        {
            _db = db;
        }

        public async Task<bool> Lagre(Billett innBillett)
        {
            if (ModelState.IsValid)
            {
                return await _db.Lagre(innBillett);
            }
        }

        public async Task<bool> HentAlle()
        {
            return await _db.HentAlle();  
        }

        public async Task<bool> HentEn(int id)
        {
            return await _db.HentEn(id);  
        }

        public async Task<bool> Endre(Billett endreBillett)
        {
            if (ModelState.IsValid)
            {
                return await _db.Endre(endreBillett);
            }
        }

        public async Task<bool> Slett(int id)
        {
            return await _db.Slett(id);
        }
}
        