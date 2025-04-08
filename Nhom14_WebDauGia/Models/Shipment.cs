using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AuctionWeb.Models
{
    public class Shipment
    {
        [Key]
        public int Shipment_ID { get; set; }

        [Required]
        [Display(Name = "Ngày giao dự kiến")]
        public DateTime Shipment_Planned_Date { get; set; }

        [Display(Name = "Ngày giao thực tế")]
        public DateTime Shipment_Actual_Date { get; set; }

        [Display(Name = "Phí giao hàng")]
        public decimal? Shipment_Fee { get; set; }

        public int Shipment_Item_ID { get; set; }

        [ForeignKey("Shipment_Item_ID")]
        public Product Product { get; set; }
    }
}


