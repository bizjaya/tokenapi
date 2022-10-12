using Microsoft.Extensions.Caching.Memory;

namespace TOKENAPI.Common
{
    public static class Const
    {
        public const string Transfer = "Transfer";
        public const string Approval = "Approval";
        public const string Claimed = "Claimed";
        public const string Purchased = "Purchased";
        public const string Staked = "Staked";
        public const string Unstaked = "Unstaked";
        public const string ContAbi = "ContAbi";
        public const string ContBCode = "ContBCode";

        public const string ContAddr = "ContAddr";
        public const string PrevBlock = "PrevBlock";

        public const string TblAcct = "acct";
        public const string TblLevel = "level";
        public const string TblStats = "stats";

        public const string TblEvtApproval = "evt_approval";
        public const string TblEvtClaimed = "evt_claimed";
        public const string TblEvtPurchased = "evt_purchased";
        public const string TblEvtStaked = "evt_staked";
        public const string TblEvtTransfer = "evt_transfer";
        public const string TblEvtUnstaked = "evt_unstaked";
        public const string TblProStaked = "pro_staked";
        public const string TblProUnstaked = "pro_unstaked";
        public const string TblProClaimed = "pro_claimed";
        public const string TblProRefCom = "pro_refcom";
        public const string TblProPoolCom = "pro_poolcom";




        /*
        reward is 0.3% per day which is 0.003 in fraction
        reward is 0.003/86400s = 0.00000003472222 % per second because there are 86400s in a day
        reward is 0.00000003472222 fr per second /1     
        for simplicity inverse / 1 it to 28800001; per second
        for testing  divide /24 1 it to 1200000 so it happens per hour rather than per day
        */

        public static decimal RPD = 0.3m; //Reward Pct Daily
        public static decimal FracPS => (RPD * 0.01m / 86400 ); //0.000000034722222m;
        /*  Percentage to Fraction per second.  */
        public static decimal Pct2FPS(decimal? pct = null) => (pct == null) ? FracPS : (FracPS * (pct ?? 0) * 0.01m);
        public static decimal Pct2FPH(decimal? pct = null) => (pct == null) ? FracPS * 3600 : (FracPS * (pct ?? 0) * 0.01m * 3600);



        public static MemoryCacheEntryOptions MemOpt => new MemoryCacheEntryOptions ()
        {
            AbsoluteExpiration = DateTime.Now.AddYears(3),
            Priority = CacheItemPriority.High,
            SlidingExpiration = TimeSpan.FromDays(700)
        };



    }
}
