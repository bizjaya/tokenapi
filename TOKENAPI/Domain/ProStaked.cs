using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TOKENAPI.Common;

namespace TOKENAPI.Domain
{
    [Table(Const.TblProStaked)]
    public class ProStaked
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
