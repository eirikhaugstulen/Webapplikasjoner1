using System;
using Microsoft.EntityFrameworkCore;

namespace Webapplikasjoner1.Model
{
    public class BillettKontekst : DbContext 
    {
        public BillettKontekst(DbContextOptions<BillettKontekst> options)
            : base (options)
            {
                Database.EnsureCreated();
            }
            
            public DbSet<Billett> Billett { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder){
                optionsBuilder.UseLazyLoadingProxies();
            }
    }

    