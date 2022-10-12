namespace TOKENAPI.DTO
{
    public class StatDto
    {

        public long Id { get; set; }


        public DateTime Hourly_LR { get; set; }
        public ulong Hourly_LRMK { get; set; }
        public DateTime Hourly_NR { get; set; }
        public ulong Hourly_NRMK { get; set; }



        public DateTime Daily_LR { get; set; }
        public ulong Daily_LRMK { get; set; }
        public DateTime Daily_NR { get; set; }
        public ulong Daily_NRMK { get; set; }

    
    }
}
