using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TOKENAPI.DTO
{
    public class StkDto
    {
        public string Type { get; set; }
        public long Id { get; set; }
        public bool proc { get; set; }
        public string? user { get; set; }
        public double amount { get; set; }
        public decimal amount_ { get; set; }
        public double total { get; set; }
        public decimal total_ { get; set; }
        public double unamt { get; set; }
        public decimal unamt_ { get; set; }
        public double timestamp { get; set; }
        public DateTime timestamp_ { get; set; }
        public ulong crtd { get; set; }
        public DateTime crtd_ { get; set; }
        public ulong blockno { get; set; }
        public string? txid { get; set; }
    }
}
