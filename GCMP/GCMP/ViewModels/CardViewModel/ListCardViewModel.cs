using System;
using System.ComponentModel.DataAnnotations;

namespace GCMP.ViewModels.CardViewModel
{
    public class ListCardAdminViewModel
    {
        [Required]
        [Display(Name = "Tên Thẻ")]
        public string CardName { get; set; }
        [Display(Name = "Ngày thêm vào")]
        public DateTime? DateAdded { get; set; }
        [Display(Name = "Tình trạng")]
        public string Status { get; set; }
        [Display(Name = "Người bán")]
        public string UserAddName { get; set; }
        [Display(Name = "Giá bán")]
        public string Price { get; set; }
    }
}