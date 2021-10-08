using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Controllers
{
    [Route("[controller]/[action]")]

    public class ReiseController : ControllerBase
    {
        //Filen er for feilhåndtering av programmet
        private IReiseRepository _db;

        private ILogger<ReiseController> _log;

        public ReiseController(IReiseRepository db, ILogger<ReiseController> log)
        {
            _db = db;
            _log = log;
        }

        //Modelstate sjekker om regexen er ok og sender feilmelding hvis ikke
        public async Task<ActionResult> Bestille(Reise innReis)
        {
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Bestille(innReis);
                if (!returOK)
                {
                    _log.LogInformation("Bestillingen ble ikke lagret");
                    return BadRequest("Bestillingen ble ikke lagret");
                }
                return Ok("Bestilling ble lagret");
              
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
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
            if (ModelState.IsValid)
            {
                Reise reisen = await _db.HentEn(id);
                if (reisen == null)
                {
                    _log.LogInformation("Fant ikke reisen");
                    return NotFound("Fant ikke reisen");
                }
                return Ok(reisen);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> Endre(Reise endreReise)
        {
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Endre(endreReise);
                if (!returOK)
                {
                    _log.LogInformation("Reise ble ikke endret");
                    return NotFound("Reise ble ikke endret");
                }
                return Ok("Reise ble endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }
    }

}
