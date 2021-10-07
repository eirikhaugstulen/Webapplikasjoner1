using Microsoft.EntityFrameworkCore;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
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

    