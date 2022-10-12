using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.ComponentModel;
using TOKENAPI.Common;

namespace TOKENAPI.Domain
{
    [Table(Const.TblEvtApproval)]
    public class EvtApproval
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool proc { get; set; }

        [StringLength(maximumLength: 300)]
        public string? owner { get; set; }

        public double bnbamt { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]

        public decimal bnbamt_ { get; set; }
        public double tokenamt { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]

        public decimal tokenamt_ { get; set; }


        public long crtd { get; set; }
        public DateTime crtd_ { get; set; }
        public long blockno { get; set; }

        [StringLength(maximumLength: 500)]
        public string? txid { get; set; }





    }
}
