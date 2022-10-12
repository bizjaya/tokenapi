using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Reflection;
using TOKENAPI.Domain;
using TOKENAPI.Common;

namespace TOKENAPI.EF
{
    public class DbCtx : DbContext
    {


        public DbCtx(DbContextOptions<DbCtx> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                // EF Core 1 & 2
                // property.Relational().ColumnType = "decimal(18, 6)";

                // EF Core 3

                var memberInfo = property.PropertyInfo ?? (MemberInfo)property.FieldInfo;
                if (memberInfo == null) continue;
                var column = Attribute.GetCustomAttribute(memberInfo, typeof(ColumnAttribute)) as ColumnAttribute;
                if (column == null) continue;
                property.SetColumnType(column.TypeName);

                // property.SetColumnType("decimal(32, 10)");
            }

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    var memberInfo = property.PropertyInfo ?? (MemberInfo)property.FieldInfo;
                    if (memberInfo == null) continue;
                    var defaultValue = Attribute.GetCustomAttribute(memberInfo, typeof(DefaultValueAttribute)) as DefaultValueAttribute;
                    if (defaultValue == null) continue;
                    property.SetDefaultValueSql(defaultValue.Value.ToString());
                }
            }

 //            builder.Ignore<Margin>(); //<--- Ignore this model from being added by convention
 //           builder.AddJsonFields(); //<--- Auto add all json fields in all models


            base.OnModelCreating(builder);


            builder.Entity<Acct>().HasData(
                    new Acct { Id = 1,  RId=0, Addr = "0x7D55Ba861B5BBf5B7C9C6196dEd6962000582Dcc", UsrId=1310895264, RefId=         0, Level=1, R1=0,R2=0,R3=0,R4=0,R5=0,R6=0 },
                         new Acct { Id = 2,  RId=1, Addr = "0x8fb5BEf92cd481eCAcc87fEA4aa9470b47d19972", UsrId=6712105348, RefId=1310895264, Level=1, R1=1,R2=0,R3=0,R4=0,R5=0,R6=0 },
                         new Acct { Id = 3,  RId=1, Addr = "0x64640eACccCCFfA22Ef6FF39D9D2806Dc13D366A", UsrId=1657410923, RefId=1310895264, Level=1, R1=1,R2=0,R3=0,R4=0,R5=0,R6=0 },
                         new Acct { Id = 4,  RId=2, Addr = "0x54557b156ad1F87e44c177dC2609781EE8A449E1", UsrId=3945110278, RefId=6712105348, Level=1, R1=2,R2=1,R3=0,R4=0,R5=0,R6=0 },
                         new Acct { Id = 5,  RId=2, Addr = "0xA8f88F9051c444310A71F791f04ac38d12B82B6A", UsrId=8537692104, RefId=6712105348, Level=1, R1=2,R2=1,R3=0,R4=0,R5=0,R6=0 },                  
                        
                         new Acct { Id = 6,  RId=5, Addr = "0xAD8EB429B12aCBa6daeAB237278fAE90F231feDD", UsrId=7261081493, RefId=8537692104, Level=1, R1=5,R2=2,R3=1,R4=0,R5=0,R6=0 },               
                         new Acct { Id = 7,  RId=6, Addr = "0x1D9cDdC3F51DBD880b4B1D2F1f882b3EB13fc81f", UsrId=3749862658, RefId=7261081493, Level=1, R1=6,R2=5,R3=2,R4=1,R5=0,R6=0 },              
                         new Acct { Id = 8,  RId=6, Addr = "0x23c4adA813a80EdFCdfE7d8f7Aa95F5a4Ce98FBb", UsrId=6195102873, RefId=7261081493, Level=1, R1=6,R2=5,R3=2,R4=1,R5=0,R6=0 },                 
                       
                         new Acct { Id = 9,  RId=6, Addr = "0xEFBb333b1F19b7F953fac9F4165b827191586284", UsrId=9152106487, RefId=7261081493, Level=1, R1=6,R2=5,R3=2,R4=1,R5=0,R6=0 },
                         new Acct { Id = 10, RId=3, Addr = "0x4475a92cA5C8Aa534A5fb811Baa7dB943CFe98D0", UsrId=4710361958, RefId=1657410923, Level=1, R1=3,R2=1,R3=0,R4=0,R5=0,R6=0 },
                         new Acct { Id = 11, RId=10,Addr = "0x62e024JH89334K0De770FF24524b99FJ9F348J43", UsrId=9897761958, RefId=4710361958, Level=1, R1=10,R2=3,R3=1,R4=0,R5=0,R6=0 }
                );


        //    builder.Entity<Acct>().HasData(
        //new Acct { Id = 1, RId = 0, Addr = "0x3434fb98175F183A4b672B91030d6d854f24eEba", UsrId = 1310895264, RefId = 0, Level = 1, R1 = 0, R2 = 0, R3 = 0, R4 = 0, R5 = 0, R6 = 0 },
        //     new Acct { Id = 2, RId = 1, Addr = "0xcae8A26e390CbB93FAf3a2BB63CCdeBC6c1F8aaf", UsrId = 6712105348, RefId = 1310895264, Level = 1, R1 = 1, R2 = 0, R3 = 0, R4 = 0, R5 = 0, R6 = 0 },
        //     new Acct { Id = 3, RId = 1, Addr = "0x4c396dBd54e2413Bbd2228BFaA182e03EA3e74ea", UsrId = 1657410923, RefId = 1310895264, Level = 1, R1 = 1, R2 = 0, R3 = 0, R4 = 0, R5 = 0, R6 = 0 },
        //     new Acct { Id = 4, RId = 2, Addr = "0xeCa0E9612CCaF7f4FaEE795dDa82C1229923E5ec", UsrId = 3945110278, RefId = 6712105348, Level = 1, R1 = 2, R2 = 1, R3 = 0, R4 = 0, R5 = 0, R6 = 0 },
        //     new Acct { Id = 5, RId = 2, Addr = "0xEEe160b3A7E016128f0eE4bb24A2573FdE9776ad", UsrId = 8537692104, RefId = 6712105348, Level = 1, R1 = 2, R2 = 1, R3 = 0, R4 = 0, R5 = 0, R6 = 0 },
        //     new Acct { Id = 6, RId = 5, Addr = "0xD1C4D5287AaB329F8d7eD7488a95b09829174Dbc", UsrId = 7261081493, RefId = 8537692104, Level = 1, R1 = 5, R2 = 2, R3 = 1, R4 = 0, R5 = 0, R6 = 0 },
        //     new Acct { Id = 7, RId = 6, Addr = "0x073Fe851Ca2649111c3ee90E23413a9C69Ac01ae", UsrId = 3749862658, RefId = 7261081493, Level = 1, R1 = 6, R2 = 5, R3 = 2, R4 = 1, R5 = 0, R6 = 0 },
        //     new Acct { Id = 8, RId = 6, Addr = "0xa00163a41c8cDcd82499DFaD05491Cde2f7Fefbe", UsrId = 6195102873, RefId = 7261081493, Level = 1, R1 = 6, R2 = 5, R3 = 2, R4 = 1, R5 = 0, R6 = 0 },
        //     new Acct { Id = 9, RId = 6, Addr = "0x27a41f7f15a59F0096Cf67d89d0Db449Fc10cDad", UsrId = 9152106487, RefId = 7261081493, Level = 1, R1 = 6, R2 = 5, R3 = 2, R4 = 1, R5 = 0, R6 = 0 },
        //     new Acct { Id = 10, RId = 3, Addr = "0x62e02429387161fa950E2DDb35fDe770FF24524b", UsrId = 4710361958, RefId = 1657410923, Level = 1, R1 = 3, R2 = 1, R3 = 0, R4 = 0, R5 = 0, R6 = 0 },
        //     new Acct { Id = 11, RId = 10, Addr = "0x62e024JH89334K0De770FF24524b99FJ9F348J43", UsrId = 9897761958, RefId = 4710361958, Level = 1, R1 = 10, R2 = 3, R3 = 1, R4 = 0, R5 = 0, R6 = 0 }
        //    );

            builder.Entity<Level>().HasData(
                        new Level
                        {
                            Id = 1,
                            MINS = 10000m,
                            RPS = Const.Pct2FPS(),
                            RPH = Const.Pct2FPH(),
                            RLOC = 0.15m,
                            PLOC = (0.05m/24),
                            PRPP = Const.Pct2FPH(5m), //because 5pct per day
                            POOLMIN = 300000m,


                            LN_1 = "Level 1",
                            LM_1 = 50000m,
                            LP_1 = 5m,
                            LU_1 = 125m,
                            FPS_1 = Const.Pct2FPS(5m),
                            FPH_1 = Const.Pct2FPH(5m),


                            LN_2 = "Level 2",
                            LM_2 = 200000m,                          
                            LP_2 = 4m,
                            LU_2 = 500m,
                            FPS_2 = Const.Pct2FPS(4m),
                            FPH_2 = Const.Pct2FPH(4m),

                            LN_3 = "Level 3",
                            LM_3 = 500000m,
                            LP_3 = 3m,
                            LU_3 = 1250m,
                            FPS_3 = Const.Pct2FPS(3m),
                            FPH_3 = Const.Pct2FPH(3m),

                            LN_4 = "Level 4",
                            LM_4 = 1000000m,
                            LP_4 = 2m,
                            LU_4 = 2500m,
                            FPS_4 = Const.Pct2FPS(2m),
                            FPH_4 = Const.Pct2FPH(2m),

                            LN_5 = "Level 5",
                            LM_5 = 2000000m,
                            LP_5 = 1m,
                            LU_5 = 5000m,
                            FPS_5 = Const.Pct2FPS(1m),
                            FPH_5 = Const.Pct2FPH(1m),

                            LN_6 = "Level 6",
                            LM_6 = 3000000m,
                            LP_6 = 0.5m,
                            LU_6 = 10000m,
                            FPS_6 = Const.Pct2FPS(0.5m),
                            FPH_6 = Const.Pct2FPH(0.5m),
                        }
              );

            builder.Entity<Stats>().HasData(
                    new Stats
                    {
                        Id=1,
                        TknSupply = 10000000000m,
                        TknPrc = 0.0005m,
                        WdrwFee = 10.0m, //inpct
                        WdrwMin = 1.0m, //in dollar
                    }
                );




        }

        public DbSet<Acct> Accts { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Stats> Stats { get; set; }

        public DbSet<EvtApproval> EvtApproval { get; set; }
        public DbSet<EvtClaimed> EvtClaimed { get; set; }
        public DbSet<EvtPurchased> EvtPurchased { get; set; }
        public DbSet<EvtStaked> EvtStaked { get; set; }
        public DbSet<EvtTransfer> EvtTransfer { get; set; }
        public DbSet<EvtUnstaked> EvtUnstaked { get; set; }

        public DbSet<ProStaked> ProStaked { get; set; }
        public DbSet<ProUnstaked> ProUnstaked { get; set; }
        public DbSet<ProClaimed> ProClaimed { get; set; }
        public DbSet<ProRefCom> ProRefCom { get; set; }
        public DbSet<ProPoolCom> ProPoolCom { get; set; }







    }
}
