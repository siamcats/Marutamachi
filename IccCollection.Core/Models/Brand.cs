using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IccCollection.Core.Models
{
    public class IccContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=iccc.db");
    }

    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ThemeId { get; set; }

        public string ThemeColor1 { get; set; }

        public string ThemeColor2 { get; set; }

        public string ThemeColor3 { get; set; }

        public string ThemeColor4 { get; set; }

        public string ThemeColor5 { get; set; }


        public List<Card> Collections { get; set; }
    }

    public class Card
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Memo { get; set; }

        public decimal Balance { get; set; }

        public decimal BalancePoint { get; set; }

        public DateTime RegisteredDateTime { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public Brand Brand { get; set; }
    }

}
