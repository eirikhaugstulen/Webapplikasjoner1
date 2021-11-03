using Microsoft.EntityFrameworkCore;
using Webapplikasjoner1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Webapplikasjoner1.DAL
{
        public class Billetter
    {
        public int Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public bool Retur { get; set; }
        public string Type { get; set; }
        public int TotalPris { get; set; }
        public int Antall { get; set; }
        public virtual Avganger Avgang {get;set;}
        public string OrdreNummer { get; set; }
    }

    public class Strekninger {
        // Legg til, endre(endrer kun p� lokasjon pekere)- ikke prioritet og Slett
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StrekningNummer { get; set; }
        public virtual Lokasjoner FraSted { get; set; }
        public virtual Lokasjoner TilSted { get; set; }
    }

    public class Avganger
    { // Legg til, endre(dato, klokkeslett, pris, pekere til Strekninger) og Slett
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AvgangNummer { get; set; }
        public string Dato { get; set; }
        public string Klokkeslett { get; set; }
        public int Pris { get; set; }
        public virtual Strekninger Strekning { get; set; }

    }
    public class Lokasjoner
    { // Legg til og Slett
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StedsNummer { get; set; }
        public string Stedsnavn { get; set; }
    }
    public class Adminer
    {
        public int Id {get; set;}
        public string Brukernavn {get; set;}
        public byte[] Passord {get; set;}
        public byte[] Salt {get; set;}
    }

    public class BillettKontekst : DbContext
    {
        public BillettKontekst(DbContextOptions<BillettKontekst> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Billetter> Billettene { get; set; }
        public DbSet<Adminer> Adminene {get; set;}
        public DbSet<Strekninger> Strekningene { get; set; }
        public DbSet<Avganger> Avgangene { get; set; }
        public DbSet<Lokasjoner> Lokasjonene { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            
        }
    }
}

    