namespace TOKENAPI.Events
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using System.Numerics;

    [Event("Approval")]
    public class ApprovalEvt
    {

        [Parameter("address", "owner", 1, true)]
        public string owner { get; set; }

        [Parameter("address", "spender", 2, true)]
        public string spender { get; set; }

        [Parameter("uint256", "value", 3, false)]
        public BigInteger value { get; set; }

        public BigInteger blockno { get; set; }
        public string txid { get; set; }



    }
}
