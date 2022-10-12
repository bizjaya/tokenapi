namespace TOKENAPI.DTO
{
    public class CommsDto
    {

        public long Id { get; set; }
        public string? Addr { get; set; }
        public long UsrId { get; set; }
        public byte Level { get; set; }
        public bool Staking { get; set; }
        public decimal StkAmt { get; set; }
        public decimal RefUnc { get; set; }

        public decimal PoolRwd{ get; set; }
        public decimal PoolUnc { get; set; }
        public decimal PoolPaid { get; set; }




        public decimal TS_1 { get; set; } // Total members
        public decimal TM_1 { get; set; } // Total members
        public decimal TA_1 { get; set; } // total  accrued
        public decimal TQ_1 { get; set; } //total qualified
        public decimal TU_1 { get; set; } //total unclaimed
        public decimal TP_1 { get; set; } //total paid



        public decimal TS_2 { get; set; }
        public decimal TM_2 { get; set; }
        public decimal TA_2 { get; set; }
        public decimal TQ_2 { get; set; }
        public decimal TU_2 { get; set; }
        public decimal TP_2 { get; set; }


        public decimal TS_3 { get; set; }
        public decimal TM_3 { get; set; }
        public decimal TA_3 { get; set; }
        public decimal TQ_3 { get; set; }
        public decimal TU_3 { get; set; }
        public decimal TP_3 { get; set; }



        public decimal TS_4 { get; set; }
        public decimal TM_4 { get; set; }
        public decimal TA_4 { get; set; }
        public decimal TQ_4 { get; set; }
        public decimal TU_4 { get; set; }
        public decimal TP_4 { get; set; }



        public decimal TS_5 { get; set; }
        public decimal TM_5 { get; set; }
        public decimal TA_5 { get; set; }
        public decimal TQ_5 { get; set; }
        public decimal TU_5 { get; set; }
        public decimal TP_5 { get; set; }



        public decimal TS_6 { get; set; }
        public decimal TM_6 { get; set; }
        public decimal TA_6 { get; set; }
        public decimal TQ_6 { get; set; }
        public decimal TU_6 { get; set; }
        public decimal TP_6 { get; set; }








    }
}
