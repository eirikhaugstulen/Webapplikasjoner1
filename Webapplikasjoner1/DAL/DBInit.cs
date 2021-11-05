using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();
           
            var db = serviceScope.ServiceProvider.GetService<BillettKontekst>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var lokasjon = new Lokasjoner()
            {
                StedsNummer = "cdefg",
                Stedsnavn = "Oslo",
            };
            
            var lokasjon2 = new Lokasjoner()
            {
                StedsNummer = "bcdef",
                Stedsnavn = "Bergen",
            };

            var lokasjon3 = new Lokasjoner()
            {
                StedsNummer = "abcde",
                Stedsnavn = "Kristiansand",
            };

            db.Lokasjonene.Add(lokasjon);
            db.Lokasjonene.Add(lokasjon2);

            var strekning = new Strekninger()
            {
                StrekningNummer = "strin",
                FraSted = lokasjon,
                TilSted = lokasjon2,
            };
            
            var strekning1 = new Strekninger()
            {
                StrekningNummer = "qopsd",
                FraSted = lokasjon2,
                TilSted = lokasjon,
            };
            
            var strekning2 = new Strekninger()
            {
                StrekningNummer = "mkers",
                FraSted = lokasjon3,
                TilSted = lokasjon2,
            };

            db.Strekningene.Add(strekning);
            db.Strekningene.Add(strekning1);
            db.Strekningene.Add(strekning2);


            var avgang = new Avganger()
            {
                AvgangNummer = "pqler",
                Dato = "2021-11-15",
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning = strekning,
            };

            var avgang2 = new Avganger()
            {
                AvgangNummer = "mkere",
                Dato = "2021-12-11",
                Klokkeslett = "08:10",
                Pris = 300,
                Strekning = strekning1,
            };

            var avgang3 = new Avganger()
            {
                AvgangNummer = "lpere",
                Dato = "2021-12-23",
                Klokkeslett = "15:00",
                Pris = 200,
                Strekning = strekning2,
            };

            db.Avgangene.Add(avgang);
            db.Avgangene.Add(avgang2);
            db.Avgangene.Add(avgang3);

            var kunde = new Kunder()
            {
                KundeId = "aaaaa",
                Adresse = "Skedsmovollen 51",
                Epost = "none@gmail.com",
                Fornavn = "Per",
                Etternavn = "Hansen",
                Telefonnummer = "004792345678",
            };
            var billett = new Billetter()
            {
                Retur = true,
                OrdreNummer = "mkrsl",
                Antall = 1,
                Avgang = avgang,
                TotalPris = 100,
                KundeId = kunde,
            };
            var billett2 = new Billetter()
            {
                Retur = false,
                OrdreNummer = "mkrsl",
                Antall = 1,
                Avgang = avgang2,
                TotalPris = 300,
                KundeId = kunde,
            };

            db.Billettene.Add(billett);
            db.Billettene.Add(billett2);

            var admin = new Adminer();
            admin.Brukernavn = "Adminbruker";
            string passord = "Test12345";
            byte[] salt = AdminRepository.LagSalt();
            byte[] hash = AdminRepository.LagHash(passord, salt);
            admin.Passord = hash;
            admin.Salt = salt;
            db.Adminene.Add(admin);
            
            db.SaveChanges();
        }
    }
}