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


            bool testFrasted = reg.IsMatch(innBillett.FraSted);
            bool testTilsted = reg.IsMatch(innBillett.TilSted);
            bool testFornavn = reg.IsMatch(innBillett.Fornavn);
            bool testEtternavn = regEtternavn.IsMatch(innBillett.Etternavn);

            if (!testFrasted || !testTilsted || !testFornavn || !testEtternavn || innBillett.Pris < 0 ||
                innBillett.Pris > 9999)
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
            Regex reg = new Regex(
                @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"); // Må være 8 tegn med store/små bokstaver og tall.
            bool gyldig = reg.IsMatch(test);
            if (!gyldig)
            {
                return false;
            }

            return true;
        }

        public static bool gyldigStedsnavn(string test)
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
            if (innStrekning.Id < 0 || innStrekning.FraSted < 0 || innStrekning.TilSted < 0)
            {
                return false;
            }

            return true;
        }
        public static bool AvgangValidering(Avgang innAvgang)
        {
            Regex tidReg = new Regex(@"^(20|21|22|23|[01]d|d)(([:][0-5]d){1,2})$");
            Regex datoReg = new Regex(@"\b(((0?[469]|11)/(0?[1-9]|[12]\d|30)|(0?[13578]|1[02])/(0?[1-9]|[12]\d|3[01])|0?2/(0?[1-9]|1\d|2[0-8]))/([1-9]\d{3}|\d{2})|0?2/29/([1-9]\d)?([02468][048]|[13579][26]))\b", RegexOptions.ECMAScript | RegexOptions.ExplicitCapture);

            bool testDato= datoReg.IsMatch(innAvgang.Dato);
            bool testKlokkeslett = tidReg.IsMatch(innAvgang.Klokkeslett);
            
            

            if (!testDato || !testKlokkeslett  || innAvgang.Pris < 0 || innAvgang.Pris > 9999)  
            {
                return false;
            }

            return true;
        }
}
}