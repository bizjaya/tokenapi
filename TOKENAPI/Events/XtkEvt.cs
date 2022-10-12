using Nethereum.ABI.FunctionEncoding.Attributes;
namespace TOKENAPI.Events
{
    using System.Numerics;

    public class XtkEvt
    {
        [Parameter("uint256", "bno", 1)]
        public ulong bno { get; set; }

        [Parameter("address", "addr", 2)]
        public string addr { get; set; }

        [Parameter("uint256", "amt", 3)]
        public BigInteger amt { get; set; }

        [Parameter("uint256", "ts", 4)]
        public ulong ts { get; set; }


    }
}
