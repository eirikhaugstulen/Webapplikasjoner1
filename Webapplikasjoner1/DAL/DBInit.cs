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
                Id = 1,
                StedsNavn = "Oslo",
            };
            
            var lokasjon2 = new Lokasjoner()
            {
                Id = 2,
                StedsNavn = "Bergen",
            };

            db.Lokasjonene.Add(lokasjon);
            db.Lokasjonene.Add(lokasjon2);

            var strekning = new Strekninger()
            {
                Id = 1,
                FraSted = lokasjon,
                TilSted = lokasjon2,
            };

            db.Strekningene.Add(strekning);

            var avgang = new Avganger()
            {
                Id = 1,
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning = strekning,
            };

            db.Avgangene.Add(avgang);

            var billett = new Billetter()
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Person",
                Retur = false,
                ReturDato = null,
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