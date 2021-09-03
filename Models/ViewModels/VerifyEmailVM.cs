using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class VerifyEmailVM
    {
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "驗證碼")]
        public string OTPUserInput { get; set; }
    }
}