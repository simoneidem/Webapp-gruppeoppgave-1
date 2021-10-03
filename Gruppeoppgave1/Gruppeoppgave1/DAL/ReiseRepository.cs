using Gruppeoppgave1.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.DAL
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
                nyReiseRad.Dato = innReise.Dato;
                nyReiseRad.Tid = innReise.Tid;
                nyReiseRad.Reiseid = innReise.Reiseid;

                var billettrad = new BillettInfo();
                billettrad.Voksen = innReise.Voksen;
                billettrad.Barn = innReise.Barn;
                billettrad.honnor = innReise.honnor;
                billettrad.Student = innReise.Student;
                nyReiseRad.BillettIn = billettrad;

                var transportrad = new TransportInfo();
                transportrad.Bil = innReise.Bil;
                transportrad.Motorsykkel = innReise.Motorsykkel;
                transportrad.Sykkel = innReise.Sykkel;
                nyReiseRad.TransportIn = transportrad;
               

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
                    Dato = k.Dato,
                    Tid = k.Tid,
                    Reiseid = k.Reiseid,
                    Voksen = k.BillettIn.Voksen,
                    honnor = k.BillettIn.honnor,
                    Barn = k.BillettIn.Barn,
                    Student = k.BillettIn.Student,
                    Bil = k.TransportIn.Bil,
                    Motorsykkel = k.TransportIn.Motorsykkel,
                    Sykkel = k.TransportIn.Sykkel
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
                BillettInfo enBillett = await _db.BillettInfo.FindAsync(id);
                TransportInfo enTransport = await _db.TransportInfo.FindAsync(id);
                _db.Reiser.Remove(enDBReise);
                _db.BillettInfo.Remove(enBillett);
                _db.TransportInfo.Remove(enTransport);
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
                Dato = enReise.Dato,
                Tid = enReise.Tid,
                Reiseid = enReise.Reiseid,
                Voksen = enReise.BillettIn.Voksen,
                honnor = enReise.BillettIn.honnor,
                Barn = enReise.BillettIn.Barn,
                Student = enReise.BillettIn.Student,
                Bil = enReise.TransportIn.Bil,
                Motorsykkel = enReise.TransportIn.Motorsykkel,
                Sykkel = enReise.TransportIn.Sykkel


            };
            return hentetReise;
        }

        public async Task<bool> Endre(Reise endreReise)
        {
            try
            {
                var endreObjekt = await _db.Reiser.FindAsync(endreReise.Id);
               
                endreObjekt.Type = endreReise.Type;
                endreObjekt.Strekning = endreReise.Strekning;
                endreObjekt.Dato = endreReise.Dato;
                endreObjekt.Tid = endreReise.Tid;
                endreObjekt.Reiseid = endreReise.Reiseid;
                endreObjekt.BillettIn.Voksen = endreReise.Voksen;
                endreObjekt.BillettIn.honnor = endreReise.honnor;
                endreObjekt.BillettIn.Barn = endreReise.Barn;
                endreObjekt.BillettIn.Student = endreReise.Student;
                endreObjekt.TransportIn.Bil = endreReise.Bil;
                endreObjekt.TransportIn.Motorsykkel = endreReise.Motorsykkel;
                endreObjekt.TransportIn.Sykkel = endreReise.Sykkel;

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

