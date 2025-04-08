using System.ComponentModel.DataAnnotations;
namespace AuctionWeb.Models
{
    public class ProductCategory
    {
        [Key]
        public int Product_Cate_ID { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Tên loại")]
        public string Product_Cate_Name { get; set; }
    }
}


