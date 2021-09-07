using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BFTFLoan.Models.EFModels
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=BFTFConnectionString")
        {
        }

        public virtual DbSet<Borrower> Borrower { get; set; }
        public virtual DbSet<Certificate> Certificate { get; set; }
        public virtual DbSet<Investment> Investment { get; set; }
        public virtual DbSet<Loan> Loan { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Repayment> Repayment { get; set; }
        public virtual DbSet<Resell> Resell { get; set; }
        public virtual DbSet<School> School { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Investment>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Investment>()
                .HasMany(e => e.Resell)
                .WithRequired(e => e.Investment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.Principal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan>()
                .HasOptional(e => e.Repayment)
                .WithRequired(e => e.Loan)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Member>()
                .Property(e => e.IDNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.CellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Gender)
                .IsFixedLength();

            modelBuilder.Entity<Member>()
                .HasOptional(e => e.Borrower)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Investment)
                .WithRequired(e => e.Member)
                .HasForeignKey(e => e.InvestorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Resell)
                .WithRequired(e => e.Member)
                .HasForeignKey(e => e.InvestorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Repayment>()
                .Property(e => e.MonthlyRate)
                .HasPrecision(5, 5);

            modelBuilder.Entity<Repayment>()
                .Property(e => e.AmortizationRate)
                .HasPrecision(38, 35);

            modelBuilder.Entity<Repayment>()
                .Property(e => e.CurrentPayable)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Repayment>()
                .Property(e => e.CurrentPrincipalPayable)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Repayment>()
                .Property(e => e.CurrentInterestPayable)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Repayment>()
                .Property(e => e.RemainPrincipal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Resell>()
                .Property(e => e.Bid)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Resell>()
                .Property(e => e.Ask)
                .HasPrecision(19, 4);

            modelBuilder.Entity<School>()
                .HasMany(e => e.Borrower)
                .WithRequired(e => e.School)
                .WillCascadeOnDelete(false);
        }
    }
}
