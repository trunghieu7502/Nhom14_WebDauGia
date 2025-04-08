using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AuctionWeb.Models
{
    public class PaymentMethod
    {
        [Key]
        public int Pay_Method_Code { get; set; }

        [Required, StringLength(200)]
        public string Pay_Method_Description { get; set; }

        public int Auction_ID { get; set; }

        [ForeignKey("Auction_ID")]
        public Auction Auction { get; set; }
    }
}


