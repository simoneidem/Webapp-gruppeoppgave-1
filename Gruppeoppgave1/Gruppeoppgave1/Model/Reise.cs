using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Gruppeoppgave1.Model
{
    public class Reise
    {
        //Regexen vi brukte for å ha kun nummer mellom 0-15 fant vi på
        //https://stackoverflow.com/questions/13127359/regular-expression-for-1-15
        //Regex for digitalklokke så brukte vi
        //https://www.oreilly.com/library/view/regular-expressions-cookbook/9781449327453/ch04s06.html

        //Fungerer som tabell i databasen
        public int Id { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅÀ-ú. \-/]{2,25}")]
        public string Type { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,30}")]
        public string Strekning { get; set; }
        [RegularExpression(@"^\d{2}\-\d{2}\-\d{2}$")]
        public string Dato { get; set; }
        [RegularExpression(@"^(2[0-3]|[01]?[0-9]):([0-5]?[0-9])$")]
        public string Tid { get; set; }
        [RegularExpression(@"^[A-Za-z][0-9]{4}$")]
        public string Reiseid { get; set; }
        [RegularExpression(@"^[+-]?[0-9]{1,3}(?:,?[0-9]{3})*$")]
        public int Pris { get; set; }

        public int BillettId { get; set; }
        [RegularExpression(@"^([0-9]|1[0-5])$")]
        public int Voksen { get; set; }
        [RegularExpression(@"^([0-9]|1[0-5])$")]
        public int Barn { get; set; }
        [RegularExpression(@"^([0-9]|1[0-5])$")]
        public int honnor { get; set; }
        [RegularExpression(@"^([0-9]|1[0-5])$")]
        public int Student { get; set; }

        public int TransportId { get; set; }
        [RegularExpression(@"^([0-9]|1[0-5])$")]
        public int Bil { get; set; }
        [RegularExpression(@"^([0-9]|1[0-5])$")]
        public int Motorsykkel { get; set; }
        [RegularExpression(@"^([0-9]|1[0-5])$")]
        public int Sykkel { get; set; }

    }
}
