/*
        Id (PK)
        AuctionItemId (FK)
        BidderName
        Amount
        CreatedAt
        */
using System.ComponentModel.DataAnnotations.Schema;
namespace Models {
    public class Bid
    {
        public int Id { get; set; } // Primary Key
        public int AuctionItemId { get; set; } // Foreign Key to AuctionItem
        public string BidderName { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        [ForeignKey("AuctionItemId")]
        public AuctionItem AuctionItem { get; set; }
    }
}