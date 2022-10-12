using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TOKENAPI.Common;

namespace TOKENAPI.Domain
{
    [Table(Const.TblLevel)]

    public class Level
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "decimal(18, 6)"), DefaultValue(0)]
        public decimal MINS { get; set; } //Minstake

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal RPS { get; set; } //fractional reward per sec users own stake

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal RPH { get; set; } //fractional reward per sec users own stake

        [Column(TypeName = "decimal(18, 6)"), DefaultValue(0)]
        public decimal RLOC { get; set; } //Referral Allocation for a user (in fraction)

        [Column(TypeName = "decimal(18, 6)"), DefaultValue(0)]
        public decimal PLOC { get; set; } //POOL Allocation for a user (in fraction)

        [Column(TypeName = "decimal(38, 16)"), DefaultValue(0)]
        public decimal PRPP { get; set; } //POOL Reward per period in fraction

        [Column(TypeName = "decimal(36, 12)"), DefaultValue(0)]
        public decimal POOLMIN { get; set; } // minimum staking to get pool rewards


        [StringLength(maximumLength: 300)]
        public string LN_1 { get; set; } //Level Name
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LM_1 { get; set; } //Level minimum
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LP_1 { get; set; } //Level Percent

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LU_1 { get; set; } //Level minimum in USD

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPS_1 { get; set; } //fractional reward per second

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPH_1 { get; set; } //fractional reward per hour




        [StringLength(maximumLength: 300)]
        public string LN_2 { get; set; } //Level Name
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LM_2 { get; set; } //Level minimum
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LP_2 { get; set; } //Level Percent

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LU_2 { get; set; } //Level minimum in USD

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPS_2 { get; set; }

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPH_2 { get; set; }



        [StringLength(maximumLength: 300)]
        public string LN_3 { get; set; } //Level Name
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LM_3 { get; set; } //Level minimum
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LP_3 { get; set; } //Level Percent

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LU_3 { get; set; } //Level minimum in USD

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPS_3 { get; set; }

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPH_3 { get; set; }






        [StringLength(maximumLength: 300)]
        public string LN_4 { get; set; } //Level Name
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LM_4 { get; set; } //Level minimum
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LP_4 { get; set; } //Level Percent

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LU_4 { get; set; } //Level minimum in USD

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPS_4 { get; set; }

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPH_4 { get; set; }





        [StringLength(maximumLength: 300)]
        public string LN_5 { get; set; } //Level Name
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LM_5 { get; set; } //Level minimum
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LP_5 { get; set; } //Level Percent

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LU_5 { get; set; } //Level minimum in USD

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPS_5 { get; set; }

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPH_5 { get; set; }



        [StringLength(maximumLength: 300)]
        public string LN_6 { get; set; } //Level Name
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LM_6 { get; set; } //Level minimum
        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LP_6 { get; set; } //Level Percent

        [Column(TypeName = "decimal(36, 6)"), DefaultValue(0)]
        public decimal LU_6 { get; set; } //Level minimum in USD

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPS_6 { get; set; }

        [Column(TypeName = "decimal(36, 18)"), DefaultValue(0)]
        public decimal FPH_6 { get; set; }




    }
}
