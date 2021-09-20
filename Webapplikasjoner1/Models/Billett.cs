namespace Webapplikasjoner1.Models
{
    public class Billett
    {
        public Billett()
        {
            public int id { get; set; }
            public string strekning { get; set; }
            public string fornavn { get; set; }
            public string etternavn { get; set; }
            public int antall { get; set; }
            public Date dato { get; set; }
        }
    }
}