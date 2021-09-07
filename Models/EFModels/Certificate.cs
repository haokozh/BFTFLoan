namespace BFTFLoan.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Certificate")]
    public partial class Certificate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BorrowerId { get; set; }

        public byte[] IDFront { get; set; }

        public byte[] IDBack { get; set; }

        public byte[] StudentIDFront { get; set; }

        public byte[] StudentIDBack { get; set; }

        public byte[] CreditReport { get; set; }
    }
}
