using System.ComponentModel.DataAnnotations;

namespace AuctionWeb.Models
{
    public class Admin
    {
        [Key]
        public int Admin_ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Họ và tên Admin")]
        public string Admin_FullName { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Địa chỉ Admin")]
        public string Admin_Address { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        [Display(Name = "Email Admin")]
        public string Admin_Email { get; set; }

        [Required]
        [Display(Name = "Số điện thoại Admin")]
        public int Admin_Phone { get; set; }
    }
}
