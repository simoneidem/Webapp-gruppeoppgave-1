using Gruppeoppgave1.DAL;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Http;
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

        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        public ReiseController(IReiseRepository db, ILogger<ReiseController> log)
        {
            _db = db;
            _log = log;
        }

        //Modelstate sjekker om regexen er ok og sender feilmelding hvis ikke
        public async Task<ActionResult> Bestille(Reise innReis)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Bestille(innReis);
                if (!returOK)
                {
                    _log.LogInformation("Reisen kunne ikke lagres");
                    return BadRequest("Reisen kunne ikke lagres");
                }
                return Ok("Reise lagret");
              
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> HentAlle()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            List<Reise> alleReiser = await _db.HentAlle();
            return Ok(alleReiser);
        }

        public async Task<ActionResult> Slett(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.Slett(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av reisen ble ikke utført");
                return NotFound("Sletting av reisen ble ikke utført");
            }
            return Ok("Reise slettet");
        }

        public async Task<ActionResult> HentEn(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Endre(endreReise);
                if (!returOK)
                {
                    _log.LogInformation("Endringen av reisen kunne ikke utføres");
                    return NotFound("Endringen av reisen kunne ikke utføres");
                }
                return Ok("Reise endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker");
                    HttpContext.Session.SetString(_loggetInn,_ikkeLoggetInn);
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn,_loggetInn);
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }
        
        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn,_ikkeLoggetInn);
        }
    }
}
