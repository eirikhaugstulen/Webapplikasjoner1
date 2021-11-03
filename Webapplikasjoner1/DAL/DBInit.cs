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
                StedsNummer = "1",
                Stedsnavn = "Oslo",
            };
            
            var lokasjon2 = new Lokasjoner()
            {
                StedsNummer = "2",
                Stedsnavn = "Bergen",
            };

            db.Lokasjonene.Add(lokasjon);
            db.Lokasjonene.Add(lokasjon2);

            var strekning = new Strekninger()
            {
                StrekningNummer = "12",
                FraSted = lokasjon,
                TilSted = lokasjon2,
            };
            
            var strekning1 = new Strekninger()
            {
                StrekningNummer = "123",
                FraSted = lokasjon,
                TilSted = lokasjon2,
            };
            
            var strekning2 = new Strekninger()
            {
                StrekningNummer = "1234",
                FraSted = lokasjon,
                TilSted = lokasjon2,
            };



            db.Strekningene.Add(strekning);
            db.Strekningene.Add(strekning1);
            db.Strekningene.Add(strekning2);


            var avgang = new Avganger()
            {
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning = strekning,
            };

            db.Avgangene.Add(avgang);

            var billett = new Billetter()
            {
                Id = 0,
                Fornavn = "Per",
                Etternavn = "Person",
                Retur = true,
                OrdreNummer = "1",
                Type = "Student",
                Antall = 1,
                Avgang = avgang,
                TotalPris = 100,
            };

            db.Billettene.Add(billett);

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