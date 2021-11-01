using Webapplikasjoner1.DAL;

namespace Webapplikasjoner1.Models
{
    public class Avgang
    {
        public int Id { get; set; }
        public string Dato { get; set; }
        public string Klokkeslett { get; set; }
        public int Pris { get; set; }
        public virtual Strekninger Strekning { get; set; }
    }
}