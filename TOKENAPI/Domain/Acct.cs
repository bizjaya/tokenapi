using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TOKENAPI.Common;

namespace TOKENAPI.Domain
{
    [Table(Const.TblAcct)]
    public class Acct
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {  get; set; }

        [StringLength(maximumLength: 300)]
        public string? Addr { get; set; }
        public long? UsrId { get; set; }
        public long? RId { get; set; }
        public long? RefId { get; set; }
        public byte Level { get; set; }

        [StringLength(maximumLength: 300)]
        public string? RefAddr { get; set; }
        public bool Staking { get; set; }

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal StkAmt { get; set; } //total staking current amount // Add amt+ when thre's a new stake for this user only.
        public long StkTime { get; set; } //stk time in epoch (resets on each re-stake/unstake

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal StkRewards { get; set; } //technically should be 0 since we are reading the blockchain, will be StkUnclaimed + timeinsecs * persec * StkAmt

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal StkUnc { get; set; } //unclaimed staked amt from the previous Re-stake  topup, each time a person claims, this should be reset to 0

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal StkClaimed { get; set; } // total amt of stakes claimed by this user (in the SC it might delete if the user has unstaked all, so best to have this history


        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal RefReward { get; set; } // Total earned from RefRewards, calculated based on level

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal RefClaimed { get; set; } // Total ref rewards claimed
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal RefCur { get; set; } // Total ref com for current iteration (TC1+ TC2+TC3....)

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal RefUnc { get; set; } // Total ref commission unclaimed RefUnc+ (TC1+ TC2+TC3....)

        public long LPTime { get; set; } //Last paid time

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]

        public decimal RefAlloc { get; set; } //Total referral allocation form this user's earnings (20% of the daily stake amt
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PoolAlloc { get; set; } //Total amount unqualified foor this user which goes into the pool

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PoolPlus { get; set; } //Special Pool Per Period for this user



        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PoolRwd { get; set; } // Total earned from PoolRwds

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PoolUnc { get; set; } // Total unclaimed from pool reward

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PoolPaid { get; set; } // Total paid from pool rwd


        public int TM_1 { get; set; }      // total number referred in this level
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TS_1 { get; set; }  // total staked in this level
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TA_1 { get; set; }  // total reward accrued in this level
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TQ_1 { get; set; }  // total reward qualified in this level
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TU_1 { get; set; }  // total reward unpaid/unclaimed in this level
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TC_1 { get; set; }  // total current reward (not accrued only for this instance)
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TP_1 { get; set; }  // total reward paid in this level
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PA_1 { get; set; }  // total pool allocation for this user and this level (based on unqualified ref amt)


        public int TM_2 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TS_2 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TA_2 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TQ_2 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TU_2 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TC_2 { get; set; } 
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TP_2 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PA_2 { get; set; }



        public int TM_3 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TS_3 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TA_3 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TQ_3 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TU_3 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TC_3 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TP_3 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PA_3 { get; set; }


        public int TM_4 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TS_4 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TA_4 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TQ_4 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TU_4 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TC_4 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TP_4 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PA_4 { get; set; }


        public int TM_5 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TS_5 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TA_5 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TQ_5 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TU_5 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TC_5 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TP_5 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PA_5 { get; set; }



        public int TM_6 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TS_6 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TA_6 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TQ_6 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TU_6 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TC_6 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal TP_6 { get; set; }
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal PA_6 { get; set; }

        public DateTime Uptd { get; set; }


        public int R1 { get; set; }
        public int R2 { get; set; }
        public int R3 { get; set; }
        public int R4 { get; set; }
        public int R5 { get; set; }
        public int R6 { get; set; }













    }
}
