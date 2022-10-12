using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.ComponentModel;
using TOKENAPI.Common;

namespace TOKENAPI.Domain
{
    [Table(Const.TblEvtPurchased)]
    public class EvtPurchased
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public bool proc { get; set; }


        [StringLength(maximumLength: 300)]
        public string? owner { get; set; }

        [StringLength(maximumLength: 300)]
        public string? spender { get; set; }

        public double value { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]

        public decimal value_ { get; set; }


        public long crtd { get; set; }
        public DateTime crtd_ { get; set; }
        public long blockno { get; set; }

        [StringLength(maximumLength: 500)]
        public string? txid { get; set; }



    }
}
