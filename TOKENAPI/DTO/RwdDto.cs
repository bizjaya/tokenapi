using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace TOKENAPI.DTO
{
public class RwdDto
{
        public string Type { get; set; }
        public long Id { get; set; }
        public string? user { get; set; }

        public decimal amount { get; set; }
        public decimal paid { get; set; }
        public decimal fees { get; set; }

        public decimal ethfee { get; set; }
        public DateTime crtd { get; set; }
        public string? txid { get; set; }
    }
}
