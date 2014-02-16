using System;
using System.ComponentModel.DataAnnotations;

namespace GCMP.ViewModels.CategoryViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Cần điền vào tên hàng")]
        [Display(Name = "Loại Hàng")]
        public string CategoryName { get; set; }

        [Display(Name = "Tình Trạng")]
        public string Status { get; set; }
    }
}