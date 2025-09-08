/*
    AuctionItem
        Id (PK)
        Title
        Description
        StartPrice
        CurrentPrice
        EndTime
        */
namespace Models {
    public class AuctionItem {
        public int Id {get;set;}
        public string Title {get;set;}
        public string Description {get;set;}
        public decimal StartPrice {get;set;}
        public decimal CurrentPrice {get;set;}
        public DateTime EndTime {get;set;}

        // Navigation property
        public List<Bid> Bids {get;set;} = new List<Bid>();
    }
}