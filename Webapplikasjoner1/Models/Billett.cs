using System.ComponentModel.DataAnnotations;

namespace Webapplikasjoner1.Models
{
    public class Billett
    {  
        public int Id { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string TilSted { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string FraSted { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string Fornavn { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string Etternavn { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,50}")]
        public int Antall { get; set; }
        [RegularExpression(@"[1-9]{1,3}")]
        public string Dato { get; set; }
    }
}