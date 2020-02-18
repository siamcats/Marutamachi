using System;
using System.Collections.Generic;

namespace IccCollection.Core.Models
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class IccContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<CollectionCard> CollectionCards { get; set; }
        public DbSet<Catalog> Catalog { get; set; }
        public DbSet<CatalogCard> CatalogCard { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=iccc.db");
    }

    /// <summary>
    /// カードブランドの定義（Suica,nanacoなど）
    /// </summary>
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public string NfcType { get; set; }
        public string SystemCode { get; set; }
        public string ServiceCode { get; set; }


        public List<CollectionCard> Collections { get; } = new List<CollectionCard>();
    }

    /// <summary>
    /// 所持するカード1枚1枚の情報
    /// </summary>
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

        public int CardId { get; set; }
        public Card Card { get; set; }
    }

    /// <summary>
    /// カード図鑑のカタログ定義（交通系ICカードなど。コレクションの目標単位にする）
    /// </summary>
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<CatalogCard> Cards { get; } = new List<CatalogCard>();
    }

    /// <summary>
    /// カード図鑑のカタログ定義の明細（交通系ICカードならSuica,ICOCA等）
    /// </summary>
    public class CatalogCard
    {
        public int Id { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
 
        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }
    }
}
