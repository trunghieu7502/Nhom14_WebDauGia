using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionWeb.Models
{
    public class Product
    {
        [Key]
        public int Product_ID { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [Display(Name = "Tên sản phẩm")]
        public string Product_Name { get; set; }

        [Required(ErrorMessage = "Mô tả là bắt buộc")]
        [Display(Name = "Mô tả")]
        public string Product_Description { get; set; }


        public decimal? Min_Bid_Increment { get; set; }

        [Required(ErrorMessage = "Giá khởi điểm là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá khởi điểm phải lớn hơn hoặc bằng 0")]
        [Display(Name = "Giá khởi điểm")]
        public decimal Product_Start_Bid_Amount { get; set; }

        [Required]
        public int Seller_ID { get; set; }  // 👈 bắt buộc phải có

        [Required(ErrorMessage = "Loại sản phẩm là bắt buộc")]
        [Display(Name = "Loại sản phẩm")]
        public int Product_Cate_ID { get; set; }

        [ForeignKey("Product_Cate_ID")]
        public ProductCategory? ProductCategory { get; set; }
    }
}
