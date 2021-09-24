using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Gruppeoppgave1.Model
{
    public class Reise
    {

        //Fungerer som tabell i databasen
        public int Id { get; set; }

        public string Type { get; set; }

        public string Strekning { get; set; }

        public string Tid { get; set; }

        public int BillettId { get; set; }

        public int Voksen { get; set; }

        public int Barn { get; set; }

        public int honnor { get; set; }

        public int Student { get; set; }

        public int TransportId { get; set; }

        public int Bil { get; set; }

        public int Motorsykkel { get; set; }

        public int Sykkel { get; set; }

    }
}
