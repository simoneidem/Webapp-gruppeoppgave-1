using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Gruppeoppgave1.Model
{   

    // Tabell for Reise
    [ExcludeFromCodeCoverage]
    public class Reiser
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Strekning { get; set; }

        public string Dato { get; set; }

        public string Innreise { get; set; }

        public string Tid { get; set; }

        public string Reiseid { get; set; }

        public int Pris { get; set; }

        public virtual BillettInfo BillettIn { get; set; }

        public virtual TransportInfo TransportIn { get; set; }

    }

    // Tabell for Billetter
    // Bruker DatabaseGeneratedOPtion.Identity for at tabellen skal få tildet en key automatisk
    [ExcludeFromCodeCoverage]
    public class BillettInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillettId { get; set; }

        public int Voksen { get; set; }

        public int Barn { get; set; }

        public int honnor { get; set; }

        public int Student { get; set; }
    }

    // Tabell for kjøretøy
    [ExcludeFromCodeCoverage]
    public class TransportInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransportId { get; set; }

        public int Bil { get; set; }

        public int Motorsykkel { get; set; }

        public int Sykkel { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Brukere
    {
        public int Id { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
        public byte[] Salt { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ReiseDB :DbContext
    {
        //Klassen er knytningen mellom controller og databasen

        public ReiseDB (DbContextOptions<ReiseDB> options) 
            : base (options)
        {
                Database.EnsureCreated();
        }


        public DbSet<Reiser> Reiser { get; set; }
        public DbSet<BillettInfo> BillettInfo { get; set; }
        public DbSet<TransportInfo> TransportInfo { get; set; }
        public DbSet<Brukere> Brukere { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
