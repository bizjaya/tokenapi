using System.Numerics;

namespace TOKENAPI.Models
{
    public class ToBN
    {
        public BigInteger Amount { get;set; }
        public BigInteger Fees { get; set; }
        public BigInteger ToPay { get; set; }
        public decimal paid { get; set; }
        public decimal fees { get; set; }


    }
}
