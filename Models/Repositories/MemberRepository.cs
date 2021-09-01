using BFTFLoan.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.Repositories
{
    public class MemberRepository
    {
        private AppDbContext db = new AppDbContext();

        public void Create(Member member)
        {
            db.Member.Add(member);
            db.SaveChanges();
        }

        public bool IsEmailExists(string email)
        {
            var member = db.Member.FirstOrDefault(m => m.Email == email);

            return member != null;
        }
    }
}