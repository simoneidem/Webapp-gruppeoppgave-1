﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Model
{

    // Tabell for Reise
    public class Reiser
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Strekning { get; set; }

        public string Tid { get; set; }

        public virtual BillettInfo BillettIn { get; set; }

        public virtual TransportInfo TransportIn { get; set; }

    }

    // Tabell for Billetter
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
    public class TransportInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransportId { get; set; }

        public int Bil { get; set; }

        public int Motorsykkel { get; set; }

        public int Sykkel { get; set; }
    }

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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
