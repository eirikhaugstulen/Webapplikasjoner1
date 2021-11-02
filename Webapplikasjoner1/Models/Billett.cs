using System;

namespace Webapplikasjoner1.Models
{
    public class Billett
    {  
        public int Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public bool Retur { get; set; }
        public string OrdreNummer { get; set; }
        public string Avgang { get; set; }
        public string Type { get; set; }
        public int Pris { get; set; }
        public int Antall { get; set; }
        public string Dato { get; set; }
        public string Klokkeslett { get; set; }
    }
}