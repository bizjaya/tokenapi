namespace TOKENAPI.CQRS
{
    public class StakeAdd
    {

        public string? User { get; set; }
        public decimal Amount { get; set; }
        public decimal Unclaimed { get; set; }
        public decimal Total { get; set; }
        public long Timestamp { get; set; }
        public int MaxLev { get; set; }
    }

}
