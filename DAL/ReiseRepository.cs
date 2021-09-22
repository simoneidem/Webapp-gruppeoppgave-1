
using weboppg1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace weboppg1.DAL
{
    public class ReiseRepository : IReiseRepository
    {
        private readonly ReiseDB _db;

        public ReiseRepository(ReiseDB db)
        {
            _db = db;
        }

        public async Task<bool> Bestille(Reise innReise)
        {
            try
            {
                var nyReiseRad = new Reiser();
                nyReiseRad.Type = innReise.Type;
                nyReiseRad.Strekning = innReise.Strekning;
                nyReiseRad.Tid = innReise.Tid;
                var sjekkAntall = await _db.Detaljer.FindAsync(innReise.Antall);
                var detaljersRad = new Detaljer();
                detaljersRad.Antall = innReise.Antall;
                detaljersRad.Billett = innReise.Billett;
                detaljersRad.Transport = innReise.Transport;
                nyReiseRad.Detalje = detaljersRad;

                /*var sjekkAntall = await _db.Detaljer.FindAsync(innReise.Antall);
                if (sjekkAntall == null)
                {
                    var detaljersRad = new Detaljer();
                    detaljersRad.Antall = innReise.Antall;
                    detaljersRad.Billett = innReise.Billett;
                    detaljersRad.Transport = innReise.Transport;
                    nyReiseRad.Detalje = detaljersRad;
                }
                else
                {
                    nyReiseRad.Detalje = sjekkAntall;
                }*/
                _db.Reiser.Add(nyReiseRad);
                await _db.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Reise>> HentAlle()
        {
            try
            {
                //Henter hele tabellen
                List<Reise> alleReisene = await _db.Reiser.Select(k => new Reise
                {
                    Id = k.Id,
                    Type = k.Type,
                    Strekning = k.Strekning,
                    Tid = k.Tid,
                    Antall = k.Detalje.Antall,
                    Billett = k.Detalje.Billett,
                    Transport = k.Detalje.Transport
                }).ToListAsync();
                return alleReisene;
            }
            catch
            {
                return null;
            }

        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Reiser enDBReise = await _db.Reiser.FindAsync(id);
                _db.Reiser.Remove(enDBReise);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Reise> HentEn(int id)
        {
            Reiser enReise = await _db.Reiser.FindAsync(id);
            var hentetReise = new Reise()
            {
                Id = enReise.Id,
                Type = enReise.Type,
                Strekning = enReise.Strekning,
                Tid = enReise.Tid,
                Antall = enReise.Detalje.Antall,
                Billett = enReise.Detalje.Billett,
                Transport = enReise.Detalje.Transport

            };
            return hentetReise;
        }

        public async Task<bool> Endre(Reise endreReise)
        {
            try
            {
                var endreObjekt = await _db.Reiser.FindAsync(endreReise.Id);
                if (endreObjekt.Detalje.Antall != endreReise.Antall)
                {
                    var sjekkAntall = _db.Detaljer.Find(endreReise.Antall);
                    var detaljersRad = new Detaljer();
                    detaljersRad.Antall = endreReise.Antall;
                    detaljersRad.Billett = endreReise.Billett;
                    detaljersRad.Transport = endreReise.Transport;
                    endreObjekt.Detalje = detaljersRad;
                    /*if (sjekkAntall == null)
                    {
                        var detaljersRad = new Detaljer();
                        detaljersRad.Antall = endreReise.Antall;
                        detaljersRad.Billett = endreReise.Billett;
                        detaljersRad.Transport = endreReise.Transport;
                        endreObjekt.Detalje = detaljersRad;
                    }
                    else
                    {
                        endreObjekt.Detalje.Antall = sjekkAntall.Antall;
                    }*/

                }
                endreObjekt.Type = endreReise.Type;
                endreObjekt.Strekning = endreReise.Strekning;
                endreObjekt.Tid = endreReise.Tid;
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

    }

}