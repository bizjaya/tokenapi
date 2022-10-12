namespace TOKENAPI.Events
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using System.Numerics;

    [Event("Purchased")]
    public class PurchasedEvt
    {

        [Parameter("address", "owner", 1, true)]
        public string owner { get; set; }

        [Parameter("uint256", "bnbamt", 2, true)]
        public BigInteger bnbamt { get; set; }

        [Parameter("uint256", "tokenamt", 3, false)]
        public BigInteger tokenamt { get; set; }

        public BigInteger blockno { get; set; }
        public string txid { get; set; }



    }
}
