namespace TOKENAPI.Events
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using System.Numerics;

    [Event("Unstaked")]
    public class UnstakedEvt
    {

        [Parameter("address", "from", 1, true)]
        public string user { get; set; }

        [Parameter("uint256", "amount", 2, false)]
        public BigInteger amount { get; set; }

        [Parameter("uint256", "total", 3, false)]
        public BigInteger total { get; set; }

        [Parameter("uint256", "total", 4, false)]
        public BigInteger unamt { get; set; }

        [Parameter("uint256", "timestamp", 5, false)]
        public BigInteger timestamp { get; set; }

        public BigInteger blockno { get; set; }
        public string txid { get; set; }



    }
}
