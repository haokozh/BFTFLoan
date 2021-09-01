namespace BFTFLoan.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Repayment")]
    public partial class Repayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LoanId { get; set; }

        public decimal MonthlyRate { get; set; }

        public decimal AmortizationRate { get; set; }

        public decimal CurrentPayable { get; set; }

        public decimal CurrentPrincipalPayable { get; set; }

        public decimal CurrentInterestPayable { get; set; }

        public decimal RemainPrincipal { get; set; }

        public int CurrentNumOfPeriods { get; set; }

        public int RemainNumOfPeriods { get; set; }

        public DateTime RepaymentTime { get; set; }

        public virtual Loan Loan { get; set; }
    }
}
