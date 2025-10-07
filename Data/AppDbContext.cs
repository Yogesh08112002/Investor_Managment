using Microsoft.EntityFrameworkCore;
using InvestorManagementAPI.Models;

namespace InvestorManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Investor> Investors { get; set; }
        public DbSet<InvestmentPortfolio> InvestmentPortfolios { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API configurations if needed
            modelBuilder.Entity<Investor>()
                .HasMany(i => i.InvestmentPortfolios)
                .WithOne(p => p.Investor)
                .HasForeignKey(p => p.InvestorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvestmentPortfolio>()
                .HasMany(p => p.Transactions)
                .WithOne(t => t.InvestmentPortfolio)
                .HasForeignKey(t => t.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}