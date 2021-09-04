using BFTFLoan.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.Repositories
{
    public class MemberRepository
    {
        private readonly AppDbContext db = new AppDbContext();

        #region 新增一筆 Member 資料至資料庫
        public void Create(Member member)
        {
            db.Member.Add(member);
            db.SaveChanges();
        }
        #endregion

        #region 更新一筆 Member 的 IsEmailVerified 資料
        public void UpdateIsEmailVerified(Member member)
        {
            Member updateMember = db.Member.Find(member.Id);
            updateMember.IsEmailVerified = member.IsEmailVerified;
            db.SaveChanges();
        }
        #endregion

        #region 依照帳號密碼尋找某一筆 Member 資料
        public Member FindMemberByAccountAndPassword(string account, string password)
        {
            return db.Member
                .Where(m => m.Account == account && m.Password == password)
                .FirstOrDefault();
        }
        #endregion

        #region 依照 Email 尋找某一筆 Member 資料
        public Member FindMemberByEmail(string email)
        {
            return db.Member
                .Where(m => m.Email == email)
                .FirstOrDefault();
        }
        #endregion

        #region 檢查要註冊的帳號或信箱是否存在
        public bool IsAccountExists(string account)
        {
            return db.Member
                .Where(m => m.Account == account)
                .FirstOrDefault() != null;
        }

        public bool IsEmailExists(string email)
        {
            return db.Member
                .Where(m => m.Email == email)
                .FirstOrDefault() != null;
        }
        #endregion
    }
}