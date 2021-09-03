using BFTFLoan.Attributes;
using BFTFLoan.Models.EFModels;
using BFTFLoan.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "{0}必須至少 8 個字元")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "確認密碼")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(10)]
        [Display(Name = "身分證字號")]
        [LegalIDNumber(ErrorMessage = "{0}不合法")]
        public string IDNumber { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(254)]
        [Display(Name = "電子信箱")]
        [EmailAddress(ErrorMessage = "{0}格式有誤")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(10)]
        [Display(Name = "手機號碼")]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(1)]
        [Display(Name = "性別")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "生日")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }

    public static class RegisterAssembler
    {
        public static Member ViewModelToEntity(this RegisterVM viewModel, bool isEmailVerified)
        {
            return new Member
            {
                // 姓名
                Name = viewModel.Name,

                // 帳號
                Account = viewModel.Account,

                // 密碼
                Password = viewModel.Password,

                // 身分證字號
                IDNumber = viewModel.IDNumber,

                // 電子信箱
                Email = viewModel.Email,

                // 手機號碼
                CellPhone = viewModel.CellPhone,

                // 性別
                Gender = viewModel.Gender,

                // 生日
                DateOfBirth = viewModel.DateOfBirth,

                // 註冊日期
                CreationTime = DateTime.Now,

                // 最後登入日期
                LastLoginTime = DateTime.Now,

                // 最後修改日期
                LastUpdateTime = DateTime.Now,

                // 是否通過 Email 驗證
                IsEmailVerified = isEmailVerified
            };
        }
    }
}