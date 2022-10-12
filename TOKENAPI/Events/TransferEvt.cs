namespace TOKENAPI.Events
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using System.Numerics;

    [Event("Transfer")]
    public class TransferEvt
    {

        [Parameter("address", "from", 1, true)]
        public string from { get; set; }

        [Parameter("address", "to", 2, true)]
        public string to { get; set; }

        [Parameter("uint256", "value", 3, false)]
        public BigInteger value { get; set; }

        public BigInteger blockno { get; set; }
        public string txid { get; set; }


    }
}
