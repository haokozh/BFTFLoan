using BFTFLoan.Attributes;
using BFTFLoan.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class ProfileVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "帳號")]
        [Editable(false)]
        public string Account { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(10)]
        [Display(Name = "身分證字號")]
        [LegalIDNumber(ErrorMessage = "{0}不合法")]
        public string IDNumber { get; set; }

        [Display(Name = "電子信箱")]
        [Editable(false)]
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
        [RangeOfDateOfBirth(MinAge = 0, MaxAge = 150, ErrorMessage = "{0}有誤")]
        public DateTime DateOfBirth { get; set; }
    }

    public static class ProfileAssembler
    {
        public static ProfileVM EntityToViewModel(this Member member)
        {
            return new ProfileVM
            {
                Id = member.Id,
                Name = member.Name,
                Account = member.Account,
                IDNumber = member.IDNumber,
                Email = member.Email,
                CellPhone = member.CellPhone,
                Gender = member.Gender,
                DateOfBirth = member.DateOfBirth
            };
        }
    }
}