namespace TOKENAPI.DTO
{
    public class UserDto
    {
        public long Id { get; set; }
        public string? Addr { get; set; }
        public long UsrId { get; set; }
        public long RId { get; set; }
        public long RefId { get; set; }
        public byte Level { get; set; }
        public string? RefAddr { get; set; }
        public bool Staking { get; set; }
        public decimal StkAmt { get; set; }

        public long StkTime { get; set; }
        public decimal StkUnc { get; set; }
        public decimal RefUnc { get; set; }
        public decimal PoolUnc { get; set; }
        public string TxId { get; set; }


    }
}
