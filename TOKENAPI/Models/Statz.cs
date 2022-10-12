using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TOKENAPI.Models
{
    public class Statz
    {
        public decimal TknPrc { get; set; } 
        public decimal WdrwFee { get; set; }
        public decimal WdrwMin { get; set; }

    }
}
