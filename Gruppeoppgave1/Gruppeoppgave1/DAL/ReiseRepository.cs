using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text;

namespace Gruppeoppgave1.DAL
{
    [ExcludeFromCodeCoverage]
    public class ReiseRepository : IReiseRepository
    {
        private readonly ReiseDB _db;
        private ILogger<ReiseRepository> _log;

        public ReiseRepository(ReiseDB db, ILogger<ReiseRepository> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<bool> Bestille(Reise innReis)
        {
            try
            {

                var nyReiseRad = new Reiser
                {
                    Type = innReis.Type,
                    Strekning = innReis.Strekning,
                    Dato = innReis.Dato,
                    Tid = innReis.Tid,
                    Reiseid = innReis.Reiseid,
                    Pris = innReis.Pris,
                    Innreise = innReis.Innreise
                };

                var billettrad = new BillettInfo
                {
                    Voksen = innReis.Voksen,
                    Barn = innReis.Barn,
                    honnor = innReis.honnor,
                    Student = innReis.Student
                };
                nyReiseRad.BillettIn = billettrad;

                var transportrad = new TransportInfo
                {
                    Bil = innReis.Bil,
                    Motorsykkel = innReis.Motorsykkel,
                    Sykkel = innReis.Sykkel
                };
                nyReiseRad.TransportIn = transportrad;
               

                _db.Reiser.Add(nyReiseRad);
                _log.LogInformation(_db.Entry(nyReiseRad).State.ToString());
                await _db.SaveChangesAsync();
                _log.LogInformation(_db.Entry(nyReiseRad).State.ToString());
              
                return true;

            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
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
                    Pris = k.Pris,
                    Innreise = k.Innreise,
                    Voksen = k.BillettIn.Voksen,
                    honnor = k.BillettIn.honnor,
                    Barn = k.BillettIn.Barn,
                    Student = k.BillettIn.Student,
                    Bil = k.TransportIn.Bil,
                    Motorsykkel = k.TransportIn.Motorsykkel,
                    Sykkel = k.TransportIn.Sykkel
                }).ToListAsync();
                System.Diagnostics.Debug.WriteLine("Hentet alle");
                return alleReisene;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
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
                _log.LogInformation(_db.Entry(enDBReise).State.ToString());
                _log.LogInformation(_db.Entry(enBillett).State.ToString());
                _log.LogInformation(_db.Entry(enTransport).State.ToString());
                await _db.SaveChangesAsync();
                _log.LogInformation(_db.Entry(enDBReise).State.ToString());
                _log.LogInformation(_db.Entry(enBillett).State.ToString());
                _log.LogInformation(_db.Entry(enTransport).State.ToString());
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<Reise> HentEn(int id)
        {
            try
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
                    Pris = enReise.Pris,
                    Innreise = enReise.Innreise,
                    Voksen = enReise.BillettIn.Voksen,
                    honnor = enReise.BillettIn.honnor,
                    Barn = enReise.BillettIn.Barn,
                    Student = enReise.BillettIn.Student,
                    Bil = enReise.TransportIn.Bil,
                    Motorsykkel = enReise.TransportIn.Motorsykkel,
                    Sykkel = enReise.TransportIn.Sykkel


                };
                System.Diagnostics.Debug.WriteLine("Hentet en");
                return hentetReise;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
            
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
                endreObjekt.Pris = endreReise.Pris;
                endreObjekt.Innreise = endreReise.Innreise;
                endreObjekt.BillettIn.Voksen = endreReise.Voksen;
                endreObjekt.BillettIn.honnor = endreReise.honnor;
                endreObjekt.BillettIn.Barn = endreReise.Barn;
                endreObjekt.BillettIn.Student = endreReise.Student;
                endreObjekt.TransportIn.Bil = endreReise.Bil;
                endreObjekt.TransportIn.Motorsykkel = endreReise.Motorsykkel;
                endreObjekt.TransportIn.Sykkel = endreReise.Sykkel;

            
                _log.LogInformation(_db.Entry(endreObjekt).State.ToString());
                await _db.SaveChangesAsync();
                _log.LogInformation(_db.Entry(endreObjekt).State.ToString());
                
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
            return true;
        }

        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: passord,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1000,
                numBytesRequested: 32);
        }
        
        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public async Task<bool> LoggInn(Bruker bruker)
        {
            try
            {
                Brukere funnetBruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
                byte[] hash = LagHash(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if (ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

    }

}

