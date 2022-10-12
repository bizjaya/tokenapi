using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using System.Numerics;
using System.ComponentModel;
using TOKENAPI.Common;

namespace TOKENAPI.Domain
{
    [Table(Const.TblEvtTransfer)]
    public class EvtTransfer
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public bool proc { get; set; }


        [StringLength(maximumLength: 300)]
        public string? from { get; set; }

        [StringLength(maximumLength: 300)]
        public string? to { get; set; }

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
