using System;

namespace Webapplikasjoner1.Models
{
    public class Billett
    {  
        public int Id { get; set; }
        public bool Retur { get; set; }
        public string OrdreNummer { get; set; }
        public string Avgang { get; set; }
        public string Type { get; set; }
        public int TotalPris { get; set; }
        public int Antall { get; set; }
        public string KundeId { get; set; }
        
    }
}