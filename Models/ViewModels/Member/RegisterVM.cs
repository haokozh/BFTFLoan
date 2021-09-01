using BFTFLoan.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "{0} 必填")]
        [StringLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [StringLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [StringLength(100)]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [StringLength(10)]
        [Display(Name = "身分證字號")]
        public string IDNumber { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [StringLength(254)]
        [Display(Name = "電子信箱")]
        [EmailAddress(ErrorMessage = "{0} 格式有誤")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [StringLength(10)]
        [Display(Name = "手機號碼")]
        public string CellPhone { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [StringLength(1)]
        [Display(Name = "性別")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }

    public static class RegisterAssembler
    {
        public static Member ViewModelToEntity(this RegisterVM viewModel)
        {
            return new Member
            {
                Name = viewModel.Name,
                Account = viewModel.Account,
                Password = viewModel.Password,
                IDNumber = viewModel.IDNumber,
                Email = viewModel.Email,
                CellPhone = viewModel.CellPhone,
                Gender = viewModel.Gender,
                CreationTime = DateTime.Now
            };
        }
    }
}