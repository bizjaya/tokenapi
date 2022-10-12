namespace TOKENAPI.Enums
{
    public enum TUnit : byte
    {
        NONE = 0,
        DAY = 1,
        WEEK = 2,
        MONTH = 3,
        YEAR = 4,
        HOUR = 5,
        MINUTE = 6,
    }

    public enum Event : byte
    {
        Stake = 0,
        Unstake = 1,
        Claim =2,
        Transfer = 3
    }
}
