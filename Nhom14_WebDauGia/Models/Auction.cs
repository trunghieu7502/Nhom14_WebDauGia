using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionWeb.Models
{
    public class Auction
    {
        [Key]
        public int Auction_ID { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        public DateTime Aut_Start_Date { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc")]
        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        public DateTime Aut_End_Date { get; set; }

        [Required(ErrorMessage = "Giá khởi điểm là bắt buộc")]
        [Display(Name = "Giá khởi điểm")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá khởi điểm phải lớn hơn hoặc bằng 0")]
        [Column(TypeName = "money")]
        public decimal Aut_Reserve_Price { get; set; }

        [Required(ErrorMessage = "Bước giá tối thiểu là bắt buộc")]
        [Display(Name = "Bước giá tối thiểu")]
        [Range(10000, double.MaxValue, ErrorMessage = "Bước giá tối thiểu phải lớn hơn hoặc bằng 10.000 VNĐ")]
        [Column(TypeName = "money")]
        public decimal Min_Bid_Increment { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Aut_Payment_Date { get; set; }

        [StringLength(100)]
        public string? Aut_Winner_FullName { get; set; }

        [Column(TypeName = "money")]
        public decimal? Aut_Winner_Amount { get; set; }

        [Required(ErrorMessage = "Sản phẩm là bắt buộc")]
        [Display(Name = "Sản phẩm")]
        public int Auc_Item_ID { get; set; }

        [ForeignKey("Auc_Item_ID")]
        public virtual Product? Product { get; set; }

        public DateTime? Payment_Deadline { get; set; }
        public bool IsPaid { get; set; }

    }
}
