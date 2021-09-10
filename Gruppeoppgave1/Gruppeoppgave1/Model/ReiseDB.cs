using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Model
{
    public class ReiseDB :DbContext
    {
        //Klassen er knytningen mellom controller og databasen

        public ReiseDB (DbContextOptions<ReiseDB> options) : base (options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Reise> Reiser { get; set; }
    }
}
