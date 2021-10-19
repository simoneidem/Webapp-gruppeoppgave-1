using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace Gruppeoppgave1.Model
{
    public class DBinit
    {
        // Denne filen legger inn data i databasen/tabellen når programmet starter
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ReiseDB>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var Billett1 = new BillettInfo { Voksen = 1, honnor = 0, Barn = 0, Student = 2 };
                var Billett2 = new BillettInfo { Voksen = 0, honnor = 2, Barn = 0, Student = 2 };

                var Transport1 = new TransportInfo { Bil = 1, Motorsykkel = 0, Sykkel = 0 };
                var Transport2 = new TransportInfo { Bil = 0, Motorsykkel = 0, Sykkel = 4 };


                var reise1 = new Reiser { Type = "En vei", Strekning = "Kristiandsand - Kiel", Dato = "17-05-2021", BillettIn = Billett1, TransportIn = Transport1, Innreise = "18-05-2021", Tid = "14:00", Reiseid = "C9543", Pris = 750 };
                var reise2 = new Reiser { Type = "Tur/retur", Strekning = "Oslo - Arendal", Dato = "08-10-2021", BillettIn = Billett2, TransportIn = Transport2, Innreise = "18-05-2021", Tid = "15:00", Reiseid = "A5123", Pris = 1250};

                db.Reiser.Add(reise1);
                db.Reiser.Add(reise2);

                var bruker = new Brukere();
                bruker.Brukernavn = "Admin";
                string passord = "Admin123";
                byte[] salt = ReiseRepository.LagSalt();
                byte[] hash = ReiseRepository.LagHash(passord, salt);
                bruker.Passord = hash;
                bruker.Salt = salt;
                db.Brukere.Add(bruker);

                db.SaveChanges();
            }
        }
    }
}
