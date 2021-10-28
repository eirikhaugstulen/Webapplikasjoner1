using System.Text.RegularExpressions;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.Validation
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

        public static bool gyldigBrukernavn(string test)
        {
            Regex reg = new Regex(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$");
            bool gyldig = reg.IsMatch(test);
            if (!gyldig)
            {
                return false;
            }
            return true;
        }

        public static bool gyldigPassord(string test)
        {
            Regex reg = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"); // Må være 8 tegn med store/små bokstaver og tall.
            bool gyldig = reg.IsMatch(test);
            if (!gyldig)
            {
                return false;
            }
            return true;
        }
        
}