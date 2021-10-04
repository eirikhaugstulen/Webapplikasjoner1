using System.ComponentModel.DataAnnotations;

namespace Webapplikasjoner1.Models
{
    public class Billett
    {  
        public int Id { get; set; }
        public string TilSted { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string FraSted { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string Fornavn { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string Etternavn { get; set; }
        public int Antall { get; set; }
        public string Dato { get; set; }
    }
}