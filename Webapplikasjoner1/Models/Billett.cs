namespace Webapplikasjoner1.Models
{
    public class Billett
    {  
        public int Id { get; set; }
        public string Strekning { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public int Antall { get; set; }
        public string Dato { get; set; }
    }
}