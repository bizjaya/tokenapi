using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TOKENAPI.Common;
using System.ComponentModel;

namespace TOKENAPI.Domain
{
    [Table(Const.TblProRefCom)]
    public class ProRefCom
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [StringLength(maximumLength: 300)]
        public string? user { get; set; }

        [Column(TypeName = "decimal(36, 12)"), DefaultValue(0)]
        public decimal amount { get; set; }
        [Column(TypeName = "decimal(36, 12)"), DefaultValue(0)]
        public decimal paid { get; set; }
        [Column(TypeName = "decimal(24, 6)"), DefaultValue(0)]
        public decimal fees { get; set; }

        [Column(TypeName = "decimal(24, 6)"), DefaultValue(0)]
        public decimal ethfee { get; set; }
        public DateTime crtd { get; set; }
        [StringLength(maximumLength: 500)]
        public string? txid { get; set; }
    }
}
