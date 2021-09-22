
using weboppg1.DAL;
using weboppg1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weboppg1.Controllers
{
    [Route("[controller]/[action]")]

    public class ReiseController : ControllerBase
    {
        private IReiseRepository _db;

        private ILogger<ReiseController> _log;

        public ReiseController(IReiseRepository db, ILogger<ReiseController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> Bestille(Reise innReise)
        {
            bool returOK = await _db.Bestille(innReise);
            if (!returOK)
            {
                _log.LogInformation("Bestillingen ble ikke lagret");
                return BadRequest("Bestillingen ble ikke lagret");
            }
            return Ok("Bestilling ble lagret");
        }

        public async Task<ActionResult> HentAlle()
        {
            List<Reise> alleReiser = await _db.HentAlle();
            return Ok(alleReiser);
        }

        public async Task<ActionResult> Slett(int id)
        {
            bool returOK = await _db.Slett(id);
            if (!returOK)
            {
                _log.LogInformation("Reise ble ikke slettet");
                return NotFound("Reise ble ikke slettet");
            }
            return Ok("Reise ble slettet");
        }

        public async Task<ActionResult> HentEn(int id)
        {
            Reise reisen = await _db.HentEn(id);
            if (reisen == null)
            {
                _log.LogInformation("Fant ikke reisen");
                return NotFound("Fant ikke reisen");
            }
            return Ok(reisen);
        }

        public async Task<ActionResult> Endre(Reise endreReise)
        {
            bool returOK = await _db.Endre(endreReise);
            if (!returOK)
            {
                _log.LogInformation("Reise ble ikke endret");
                return NotFound("Reise ble ikke endret");
            }
            return Ok("Reise ble endret");
        }
    }

}
