using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TOKENAPI.Domain
{
    public class EvtXtk
    {
        public ulong bno { get; set; }
        [StringLength(maximumLength: 300)]
        public string? addr { get; set; }
        public double amt { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal amt_ { get; set; }
        public double ts { get; set; }
        public DateTime ts_ { get; set; }

    }
}
