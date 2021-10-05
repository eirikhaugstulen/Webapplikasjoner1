using System;
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

            if (!testFrasted || !testTilsted || !testFornavn || !testEtternavn)
            {
                return false;
            }

            return true;
        }
    }
}