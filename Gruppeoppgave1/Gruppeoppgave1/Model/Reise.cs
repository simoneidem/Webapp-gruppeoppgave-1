﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppeoppgave1.Model
{
    public class Reise
    {

        //Fungerer som tabell i databasen
        public int Id { get; set; }

        public string Strekning { get; set; }

        public string Tid { get; set; }
    }
}