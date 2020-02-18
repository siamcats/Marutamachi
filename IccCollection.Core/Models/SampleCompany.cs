using System;
using System.Collections.Generic;

namespace IccCollection.Core.Models
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class BloggingContext : DbContext
    {
        public DbSet<Card> Blogs { get; set; }
        public DbSet<CollectionCard> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=blogging.db");
    }

    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public string NfcType { get; set; }
        public string SystemCode { get; set; }
        public string ServiceCode { get; set; }


        public List<CollectionCard> Posts { get; } = new List<CollectionCard>();
    }

    public class CollectionCard
    {
        public int Id { get; set; }
        public string Idm { get; set; }
        public string Name { get; set; }
        public string Memo { get; set; }
        public decimal Balance { get; set; }
        public decimal BalancePoint { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int BlogId { get; set; }
        public Card Blog { get; set; }
    }

    public class CollectionCatalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // TODO WTS: Remove this class once your pages/features are using your data.
    // This is used by the SampleDataService.
    // It is the model class we use to display data on pages like Grid, Chart, and Master Detail.
    public class SampleCompany
    {
        public string CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public ICollection<SampleOrder> Orders { get; set; }
    }
}
