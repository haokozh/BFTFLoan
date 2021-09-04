using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "新密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "{0}必須至少 8 個字元")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "確認新密碼")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "{0}不相符")]
        public string ConfirmPassword { get; set; }
    }
}