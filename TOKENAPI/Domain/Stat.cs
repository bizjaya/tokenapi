using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TOKENAPI.Common;

namespace TOKENAPI.Domain
{
 
    [Table(Const.TblStats)]
    public class Stats
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }


        public DateTime Hourly_LR { get; set; }
        public ulong Hourly_LRMK { get; set; }
        public DateTime Hourly_NR { get; set; }
        public ulong Hourly_NRMK { get; set; }



        public DateTime Daily_LR { get; set; }
        public ulong Daily_LRMK { get; set; }
        public DateTime Daily_NR { get; set; }
        public ulong Daily_NRMK { get; set; }



        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TknSupply { get; set; }

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal RwdPaid { get; set; } 

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal RefPaid { get; set; } 

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PoolPaid { get; set; } 


        [Column(TypeName = "decimal(24, 12)"), DefaultValue(0)]
        public decimal TknPrc { get; set; } //Token price in USD

        [Column(TypeName = "decimal(8, 4)"), DefaultValue(0)]
        public decimal WdrwFee { get; set; } //Ref Comm Wdraw Fee (1) ?

        [Column(TypeName = "decimal(8, 4)"), DefaultValue(0)]
        public decimal WdrwMin { get; set; } //Min withdraw of RefComm in USD








    }
}
