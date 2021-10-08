using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                var context = serviceScope.ServiceProvider.GetService<ReiseDB>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var Billett1 = new BillettInfo { Voksen = 1, honnor = 0, Barn = 0, Student = 2 };
                var Billett2 = new BillettInfo { Voksen = 0, honnor = 2, Barn = 0, Student = 2 };

                var Transport1 = new TransportInfo { Bil = 1, Motorsykkel = 0, Sykkel = 0 };
                var Transport2 = new TransportInfo { Bil = 0, Motorsykkel = 0, Sykkel = 4 };


                var reise1 = new Reiser { Type = "En vei", Strekning = "Kristiandsand - Kiel", Dato = "17-05-2021", BillettIn = Billett1, TransportIn = Transport1, Innreise = "18-05-2021" };
                var reise2 = new Reiser { Type = "Tur/retur", Strekning = "Oslo - Arendal", Dato = "08-10-2021", BillettIn = Billett2, TransportIn = Transport2, Innreise = "18-05-2021" };

                context.Reiser.Add(reise1);
                context.Reiser.Add(reise2);

                context.SaveChanges();
            }
        }
    }
}
