using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TOKENAPI.Common;

namespace TOKENAPI.Domain
{

    [Table(Const.TblProClaimed)]
    public class ProClaimed
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public bool proc { get; set; }


        [StringLength(maximumLength: 300)]
        public string? user { get; set; }
        public double amount { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal amount_ { get; set; }
        public double total { get; set; }

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal total_ { get; set; }

        public double unamt { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal unamt_ { get; set; }

        public double timestamp { get; set; }
        public DateTime timestamp_ { get; set; }

        public long crtd { get; set; }
        public DateTime crtd_ { get; set; }
        public long blockno { get; set; }

        [StringLength(maximumLength: 500)]
        public string? txid { get; set; }

    }
}
