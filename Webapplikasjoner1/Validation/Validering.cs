using System.Text.RegularExpressions;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.Validation
{
    public class Validering
    {
        public static bool BillettValidering(Billett innBillett)
        {
            Regex reg = new Regex(@"[a-zA-ZæøåÆØÅ. \-]{2,20}");
            Regex regEtternavn = new Regex(@"[a-zA-ZæøåÆØÅ. \-]{2,50}");
            
            bool testFornavn = reg.IsMatch(innBillett.Fornavn);
            bool testEtternavn = regEtternavn.IsMatch(innBillett.Etternavn);
            bool testType = reg.IsMatch(innBillett.Type);

            if (!testType || !testFornavn || !testEtternavn || innBillett.TotalPris <= 0 || 
                innBillett.Antall<0)
            {
                return false;
            }

            return true;
        }

        public static bool GyldigBrukernavn(string test)
        {
            Regex reg = new Regex(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$");
            bool gyldig = reg.IsMatch(test);
            if (!gyldig)
            {
                return false;
            }
            return true;
        }

        public static bool GyldigPassord(string test)
        {
            Regex reg = new Regex(
                @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"); // Må være 8 tegn med store/små bokstaver og tall.
            bool gyldig = reg.IsMatch(test);
            if (!gyldig)
            {
                return false;
            }

            return true;
        }

        public static bool GyldigStedsnavn(string test)
        {
            Regex reg = new Regex(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$");
            bool gyldig = reg.IsMatch(test);
            if (!gyldig)
            {
                return false;
            }
            return true;
        }

        public static bool StrekningValidering(Strekning innStrekning)
        {
            if (innStrekning.StrekningNummer ==null || innStrekning.TilSted == null || innStrekning.FraSted == null)
            {
                return false;
            }

            return true;
        }
        public static bool AvgangValidering(Avgang innAvgang)
        {
            
            if  (innAvgang.Pris < 0 || innAvgang.Pris > 9999 || innAvgang.Dato == null || innAvgang.Klokkeslett ==null 
                 || innAvgang.Strekning == null)  
            {
                return false;
            }

            return true;
        }
}
}