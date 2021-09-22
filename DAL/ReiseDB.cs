
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace weboppg1.DAL
{

    public class Reiser
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Strekning { get; set; }
        public string Tid { get; set; }
        virtual public Detaljer Detalje { get; set; }
    }

    public class Detaljer
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(DatabaseGeneratedOption.None)]

        public string Antall { get; set; }
        public string Billett { get; set; }
        public string Transport { get; set; }
    }
    public class ReiseDB : DbContext
    {
        //Klassen er knytningen mellom controller og databasen

        public ReiseDB(DbContextOptions<ReiseDB> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Reiser> Reiser { get; set; }

        public DbSet<Detaljer> Detaljer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
