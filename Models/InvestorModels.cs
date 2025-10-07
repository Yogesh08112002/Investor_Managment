using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InvestorManagementAPI.Models
{
    public class Investor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(15)]
        public string ContactNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        // Navigation property: One Investor can have multiple portfolios (non-nullable, initialized)
        public ICollection<InvestmentPortfolio> InvestmentPortfolios { get; set; } = new List<InvestmentPortfolio>();
    }

    public class InvestmentPortfolio
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Investor")]
        public int InvestorId { get; set; }

        [Required]
        [StringLength(50)]
        public string AssetType { get; set; } = string.Empty; // e.g., Equity, Mutual Fund

        [Required]
        [StringLength(100)]
        public string AssetName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; }

        public DateTime PurchaseDate { get; set; }

        // Navigation property - ignored during JSON serialization to prevent circular reference
        [JsonIgnore]
        public Investor? Investor { get; set; }

        // Navigation property: One portfolio can have multiple transactions (non-nullable, initialized)
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("InvestmentPortfolio")]
        public int PortfolioId { get; set; }

        [Required]
        [StringLength(20)]
        public string TransactionType { get; set; } = string.Empty;  // Buy, Sell, Dividend

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public DateTime TransactionDate { get; set; }

        // Navigation property - ignored during JSON serialization to prevent circular reference
        [JsonIgnore]
        public InvestmentPortfolio? InvestmentPortfolio { get; set; }
    }
}
