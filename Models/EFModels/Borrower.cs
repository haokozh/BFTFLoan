namespace BFTFLoan.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Borrower")]
    public partial class Borrower
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MemberId { get; set; }

        public int SchoolId { get; set; }

        [Required]
        [StringLength(10)]
        public string CreditRating { get; set; }

        public virtual Member Member { get; set; }

        public virtual School School { get; set; }
    }
}
