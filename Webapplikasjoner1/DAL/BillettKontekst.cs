using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{   public class Billetter
    {
        public int Id { get; set; }
        public string TilSted { get; set; }
        public string FraSted { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Dato { get; set; }
        
     
    }

    public class BillettKontekst : DbContext
    {
        public BillettKontekst(DbContextOptions<BillettKontekst> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Billett> Billetter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}

    