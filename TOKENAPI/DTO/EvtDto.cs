using TOKENAPI.Common;

namespace TOKENAPI.DTO
{
    public class EvtDto
    {
        public string txid { get; set; }
        public string type { get; set; }
        public ulong timestamp { get; set; }
        public ulong blockno { get; set; }

        public DateTime timestamp_ { get { return timestamp.ToDate(); } }
        public double amount { get; set; }
        public double amount_ { get; set; }
        public double unamt { get; set; }
        public double unamt_ { get; set; }

    }
}
