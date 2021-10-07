using System.Text.RegularExpressions;

namespace Webapplikasjoner1.Models
{
    public class Validering
    {
        public static bool BillettValidering(Billett innBillett)
        {
            Regex reg = new Regex(@"[a-zA-ZæøåÆØÅ. \-]{2,20}");
            Regex regEtternavn = new Regex(@"[a-zA-ZæøåÆØÅ. \-]{2,50}");
            
            
            bool testFrasted = reg.IsMatch(innBillett.FraSted);
            bool testTilsted = reg.IsMatch(innBillett.TilSted);
            bool testFornavn = reg.IsMatch(innBillett.Fornavn);
            bool testEtternavn = regEtternavn.IsMatch(innBillett.Etternavn);

            if (!testFrasted || !testTilsted || !testFornavn || !testEtternavn || innBillett.Pris < 0 || innBillett.Pris > 9999)  
            {
                return false;
            }

            return true;
        }
    }
}