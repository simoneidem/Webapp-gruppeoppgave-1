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
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ReiseDB>();

                // må slette og opprette databasen hver gang når den skalinitieres (seed`es)
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var detaljer1 = new Detaljer { Antall = "2", Billett = "Student", Transport = "ingen" };
                var detaljer2 = new Detaljer { Antall = "1", Billett = "Voksen", Transport = "Bil"};

                var reise1 = new Reiser { Type = "En vei", Strekning = "Kristiandsand - Kiel", Tid = "09:00", Detalje = detaljer1 };
                var reise2 = new Reiser { Type = "Tur/retur", Strekning = "Oslo - Arendal", Tid = "12:00", Detalje = detaljer2 };

                context.Reiser.Add(reise1);
                context.Reiser.Add(reise2);

                context.SaveChanges();
            }
        }
    }
}
