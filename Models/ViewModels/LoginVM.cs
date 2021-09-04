using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "{0}必須至少 8 個字元")]
        public string Password { get; set; }

        [Display(Name = "記住密碼")]
        public bool RemeberMe { get; set; }
    }
}