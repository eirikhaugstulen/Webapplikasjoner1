namespace Webapplikasjoner1.Models
{
    public class Billett
    {  
        public int Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public bool Retur { get; set; }
        public int ReturDato { get; set; }
        public int Avgang { get; set; }
        public string Type { get; set; }
        public int Pris { get; set; }
        public int Antall { get; set; }
    }
}