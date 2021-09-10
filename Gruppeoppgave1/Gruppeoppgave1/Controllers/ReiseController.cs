using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Controllers
{
    [Route("[controller]/[action]")]
    
    public class ReiseController : ControllerBase
    {
        private readonly ReiseDB _db;

        public ReiseController(ReiseDB db)
        {
            _db = db;
        }

        public bool Bestille(Reise innReise)
        {
            try
            {
                _db.Reiser.Add(innReise);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Reise> HentAlle()
        {
            try
            {
                //Henter hele tabellen
                List<Reise> alleReisene = _db.Reiser.ToList();
                return alleReisene;
            }
            catch
            {
                return null;
            }
            
        }

        public bool Slett(int id)
        {
            try
            {
                Reise enReise = _db.Reiser.Find(id);
                _db.Reiser.Remove(enReise);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Reise HentEn(int id)
        {
            try
            {
                Reise enReise = _db.Reiser.Find(id);
                return enReise;
            }
            catch
            {
                return null;
            }

        }

        public bool Endre(Reise endreReise)
        {
            try
            {
                Reise enReise = _db.Reiser.Find(endreReise.Id);
                enReise.Strekning = endreReise.Strekning;
                enReise.Tid = endreReise.Tid;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
