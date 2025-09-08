namespace Models {
    using Microsoft.EntityFrameworkCore;
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AuctionItem> AuctionItems { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship between AuctionItem and Bid
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.AuctionItem)
                .WithMany(ai => ai.Bids)
                .HasForeignKey(b => b.AuctionItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuctionItem>().HasKey(ai => ai.Id);
            modelBuilder.Entity<Bid>().HasKey(b => b.Id);

            modelBuilder.Entity<AuctionItem>().HasData(
                new AuctionItem { Id = 1, Title = "Vintage Clock", Description = "An old vintage clock.", StartPrice = 50.00m, CurrentPrice = 50.00m, EndTime = DateTime.Now.AddDays(7) },
                new AuctionItem { Id = 2, Title = "Antique Vase", Description = "A beautiful antique vase.", StartPrice = 100.00m, CurrentPrice = 100.00m, EndTime = DateTime.Now.AddDays(5) }
            );

            modelBuilder.Entity<Bid>().HasData(
                new Bid { Id = 1, AuctionItemId = 1, BidderName = "Alice", Amount = 60.00m, CreatedAt = DateTime.Now.AddHours(-2) },
                new Bid { Id = 2, AuctionItemId = 1, BidderName = "Bob", Amount = 70.00m, CreatedAt = DateTime.Now.AddHours(-1) },
                new Bid { Id = 3, AuctionItemId = 2, BidderName = "Charlie", Amount = 120.00m, CreatedAt = DateTime.Now.AddHours(-3) }
            );
        }
    }
}