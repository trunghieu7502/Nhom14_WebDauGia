using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionWeb.Models
{
    public class AuctionBid
    {
        [Key]
        public int Bid_ID { get; set; }

        public int Auction_ID { get; set; }
        [ForeignKey("Auction_ID")]
        public Auction? Auction { get; set; }

        public int User_ID { get; set; }
        [ForeignKey("User_ID")]
        public UserAccount? UserAccount { get; set; }

        [Display(Name = "Số tiền đấu giá")]

        public decimal Bid_Amount { get; set; }

        [Display(Name = "Thời điểm đấu giá")]
        public DateTime Bid_Time { get; set; }
    }
}
