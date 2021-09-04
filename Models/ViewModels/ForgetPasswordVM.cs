using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class ForgetPasswordVM
    {
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(254)]
        [Display(Name = "電子信箱")]
        [EmailAddress(ErrorMessage = "{0}格式有誤")]
        public string Email { get; set; }
    }
}