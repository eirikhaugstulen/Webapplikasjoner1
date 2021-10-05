using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Webapplikasjoner1.Models
{
    public class Billett
    {  
        public int Id { get; set; }
        public string TilSted { get; set; }
        public string FraSted { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Dato { get; set; }
        public bool Retur { get; set; }
        public string ReturDato { get; set; }
        public int Pris { get; set; }
    }
}