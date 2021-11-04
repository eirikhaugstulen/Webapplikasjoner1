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
            
            bool testType = reg.IsMatch(innBillett.Type);

            if ( innBillett.TotalPris <= 0 || 
                innBillett.Antall<0 || !testType)
            {
                return false;
            }

            return true;
        }

        public static bool KundeValidering(Kunde innKunde)
        {
            Regex reg = new Regex(@"[a-zA-ZæøåÆØÅ. \-]{2,20}");
            Regex regEtternavn = new Regex(@"[a-zA-ZæøåÆØÅ. \-]{2,50}");
            Regex regAdresse = new Regex(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,50}");
            Regex regEpost = new Regex(@"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$");
            // Regexen under er hentet fra https://www.epinova.no/folg-med/blogg/2020/regex-huskeliste-for-norske-formater-i-episerver-forms/ den 04.11.21
            Regex regTelefon = new Regex(@"^((0047)?|(\+47)?)[1-9]\d{7}$");

            bool testFornavn = reg.IsMatch(innKunde.Fornavn);
            bool testEtternavn = regEtternavn.IsMatch(innKunde.Etternavn);
            bool testAdresse = regAdresse.IsMatch(innKunde.Adresse);
            bool testEpost = regEpost.IsMatch(innKunde.Epost);
            bool testTelefon = regTelefon.IsMatch(innKunde.Telefonnummer);

            if (!testAdresse || !testFornavn || !testEtternavn || !testEpost || !testTelefon)
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
            
            if  (innAvgang.Pris < 0 || innAvgang.Pris > 9999)  
            {
                return false;
            }

            return true;
        }
}
}