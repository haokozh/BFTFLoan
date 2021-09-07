namespace BFTFLoan.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resell")]
    public partial class Resell
    {
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public int InvestmentId { get; set; }

        public decimal Bid { get; set; }

        public decimal Ask { get; set; }

        public virtual Investment Investment { get; set; }

        public virtual Member Member { get; set; }
    }
}
