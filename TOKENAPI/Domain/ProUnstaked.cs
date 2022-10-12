using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TOKENAPI.Common;

namespace TOKENAPI.Domain
{
    [Table(Const.TblProUnstaked)]
    public class ProUnstaked
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [StringLength(maximumLength: 300)]
        public string? user { get; set; }
        public double amount { get; set; }
        public double total { get; set; }
        public double unamt { get; set; }
        public DateTime crtd { get; set; }
        public ulong blockno { get; set; }
        [StringLength(maximumLength: 500)]
        public string? txid { get; set; }
    }
}
