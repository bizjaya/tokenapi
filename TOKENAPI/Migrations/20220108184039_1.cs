using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TOKENAPI.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "acct",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Addr = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsrId = table.Column<long>(type: "bigint", nullable: true),
                    RId = table.Column<long>(type: "bigint", nullable: true),
                    RefId = table.Column<long>(type: "bigint", nullable: true),
                    Level = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    RefAddr = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Staking = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StkAmt = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    StkTime = table.Column<long>(type: "bigint", nullable: false),
                    StkRewards = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    StkUnc = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    StkClaimed = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    RefReward = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    RefClaimed = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    RefCur = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    RefUnc = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LPTime = table.Column<long>(type: "bigint", nullable: false),
                    RefAlloc = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PoolAlloc = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PoolPlus = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PoolRwd = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PoolUnc = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PoolPaid = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TM_1 = table.Column<int>(type: "int", nullable: false),
                    TS_1 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TA_1 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TQ_1 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TU_1 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TC_1 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TP_1 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PA_1 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TM_2 = table.Column<int>(type: "int", nullable: false),
                    TS_2 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TA_2 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TQ_2 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TU_2 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TC_2 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TP_2 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PA_2 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TM_3 = table.Column<int>(type: "int", nullable: false),
                    TS_3 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TA_3 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TQ_3 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TU_3 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TC_3 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TP_3 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PA_3 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TM_4 = table.Column<int>(type: "int", nullable: false),
                    TS_4 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TA_4 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TQ_4 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TU_4 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TC_4 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TP_4 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PA_4 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TM_5 = table.Column<int>(type: "int", nullable: false),
                    TS_5 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TA_5 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TQ_5 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TU_5 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TC_5 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TP_5 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PA_5 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TM_6 = table.Column<int>(type: "int", nullable: false),
                    TS_6 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TA_6 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TQ_6 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TU_6 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TC_6 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TP_6 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PA_6 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    Uptd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    R1 = table.Column<int>(type: "int", nullable: false),
                    R2 = table.Column<int>(type: "int", nullable: false),
                    R3 = table.Column<int>(type: "int", nullable: false),
                    R4 = table.Column<int>(type: "int", nullable: false),
                    R5 = table.Column<int>(type: "int", nullable: false),
                    R6 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acct", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "evt_approval",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    proc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    owner = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bnbamt = table.Column<double>(type: "double", nullable: false),
                    bnbamt_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    tokenamt = table.Column<double>(type: "double", nullable: false),
                    tokenamt_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    crtd = table.Column<long>(type: "bigint", nullable: false),
                    crtd_ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    blockno = table.Column<long>(type: "bigint", nullable: false),
                    txid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evt_approval", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "evt_claimed",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    proc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    user = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<double>(type: "double", nullable: false),
                    amount_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    total = table.Column<double>(type: "double", nullable: false),
                    total_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    unamt = table.Column<double>(type: "double", nullable: false),
                    unamt_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    timestamp = table.Column<double>(type: "double", nullable: false),
                    timestamp_ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    crtd = table.Column<long>(type: "bigint", nullable: false),
                    crtd_ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    blockno = table.Column<long>(type: "bigint", nullable: false),
                    txid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evt_claimed", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "evt_purchased",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    proc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    owner = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    spender = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<double>(type: "double", nullable: false),
                    value_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    crtd = table.Column<long>(type: "bigint", nullable: false),
                    crtd_ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    blockno = table.Column<long>(type: "bigint", nullable: false),
                    txid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evt_purchased", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "evt_staked",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    proc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    user = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<double>(type: "double", nullable: false),
                    amount_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    total = table.Column<double>(type: "double", nullable: false),
                    total_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    unamt = table.Column<double>(type: "double", nullable: false),
                    unamt_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    timestamp = table.Column<double>(type: "double", nullable: false),
                    timestamp_ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    crtd = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    crtd_ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    blockno = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    txid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evt_staked", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "evt_transfer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    proc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    from = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    to = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<double>(type: "double", nullable: false),
                    value_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    crtd = table.Column<long>(type: "bigint", nullable: false),
                    crtd_ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    blockno = table.Column<long>(type: "bigint", nullable: false),
                    txid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evt_transfer", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "evt_unstaked",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    proc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    user = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<double>(type: "double", nullable: false),
                    amount_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    total = table.Column<double>(type: "double", nullable: false),
                    total_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    unamt = table.Column<double>(type: "double", nullable: false),
                    unamt_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    timestamp = table.Column<double>(type: "double", nullable: false),
                    timestamp_ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    crtd = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    crtd_ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    blockno = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    txid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evt_unstaked", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "level",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MINS = table.Column<decimal>(type: "decimal(18,6)", nullable: false, defaultValueSql: "0"),
                    RPS = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    RPH = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    RLOC = table.Column<decimal>(type: "decimal(18,6)", nullable: false, defaultValueSql: "0"),
                    PLOC = table.Column<decimal>(type: "decimal(18,6)", nullable: false, defaultValueSql: "0"),
                    PRPP = table.Column<decimal>(type: "decimal(38,16)", nullable: false, defaultValueSql: "0"),
                    POOLMIN = table.Column<decimal>(type: "decimal(36,12)", nullable: false, defaultValueSql: "0"),
                    LN_1 = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LM_1 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LP_1 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LU_1 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    FPS_1 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    FPH_1 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    LN_2 = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LM_2 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LP_2 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LU_2 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    FPS_2 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    FPH_2 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    LN_3 = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LM_3 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LP_3 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LU_3 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    FPS_3 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    FPH_3 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    LN_4 = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LM_4 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LP_4 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LU_4 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    FPS_4 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    FPH_4 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    LN_5 = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LM_5 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LP_5 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LU_5 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    FPS_5 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    FPH_5 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    LN_6 = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LM_6 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LP_6 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    LU_6 = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    FPS_6 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0"),
                    FPH_6 = table.Column<decimal>(type: "decimal(36,18)", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pro_claimed",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    proc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    user = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<double>(type: "double", nullable: false),
                    amount_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    total = table.Column<double>(type: "double", nullable: false),
                    total_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    unamt = table.Column<double>(type: "double", nullable: false),
                    unamt_ = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    timestamp = table.Column<double>(type: "double", nullable: false),
                    timestamp_ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    crtd = table.Column<long>(type: "bigint", nullable: false),
                    crtd_ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    blockno = table.Column<long>(type: "bigint", nullable: false),
                    txid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pro_claimed", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pro_poolcom",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<decimal>(type: "decimal(36,12)", nullable: false, defaultValueSql: "0"),
                    paid = table.Column<decimal>(type: "decimal(36,12)", nullable: false, defaultValueSql: "0"),
                    fees = table.Column<decimal>(type: "decimal(24,6)", nullable: false, defaultValueSql: "0"),
                    ethfee = table.Column<decimal>(type: "decimal(24,6)", nullable: false, defaultValueSql: "0"),
                    crtd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    txid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pro_poolcom", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pro_refcom",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<decimal>(type: "decimal(36,12)", nullable: false, defaultValueSql: "0"),
                    paid = table.Column<decimal>(type: "decimal(36,12)", nullable: false, defaultValueSql: "0"),
                    fees = table.Column<decimal>(type: "decimal(24,6)", nullable: false, defaultValueSql: "0"),
                    ethfee = table.Column<decimal>(type: "decimal(24,6)", nullable: false, defaultValueSql: "0"),
                    crtd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    txid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pro_refcom", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pro_staked",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<double>(type: "double", nullable: false),
                    total = table.Column<double>(type: "double", nullable: false),
                    unamt = table.Column<double>(type: "double", nullable: false),
                    crtd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    blockno = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    txid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pro_staked", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pro_unstaked",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<double>(type: "double", nullable: false),
                    total = table.Column<double>(type: "double", nullable: false),
                    unamt = table.Column<double>(type: "double", nullable: false),
                    crtd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    blockno = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    txid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pro_unstaked", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "stats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Hourly_LR = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Hourly_LRMK = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    Hourly_NR = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Hourly_NRMK = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    Daily_LR = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Daily_LRMK = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    Daily_NR = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Daily_NRMK = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    TknSupply = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    RwdPaid = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    RefPaid = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    PoolPaid = table.Column<decimal>(type: "decimal(36,6)", nullable: false, defaultValueSql: "0"),
                    TknPrc = table.Column<decimal>(type: "decimal(24,12)", nullable: false, defaultValueSql: "0"),
                    WdrwFee = table.Column<decimal>(type: "decimal(8,4)", nullable: false, defaultValueSql: "0"),
                    WdrwMin = table.Column<decimal>(type: "decimal(8,4)", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stats", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "acct",
                columns: new[] { "Id", "Addr", "LPTime", "Level", "R1", "R2", "R3", "R4", "R5", "R6", "RId", "RefAddr", "RefId", "Staking", "StkTime", "TM_1", "TM_2", "TM_3", "TM_4", "TM_5", "TM_6", "Uptd", "UsrId" },
                values: new object[,]
                {
                    { 1L, "0x7D55Ba861B5BBf5B7C9C6196dEd6962000582Dcc", 0L, (byte)1, 0, 0, 0, 0, 0, 0, 0L, null, 0L, false, 0L, 0, 0, 0, 0, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1310895264L },
                    { 2L, "0x8fb5BEf92cd481eCAcc87fEA4aa9470b47d19972", 0L, (byte)1, 1, 0, 0, 0, 0, 0, 1L, null, 1310895264L, false, 0L, 0, 0, 0, 0, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6712105348L },
                    { 3L, "0x64640eACccCCFfA22Ef6FF39D9D2806Dc13D366A", 0L, (byte)1, 1, 0, 0, 0, 0, 0, 1L, null, 1310895264L, false, 0L, 0, 0, 0, 0, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1657410923L },
                    { 4L, "0x54557b156ad1F87e44c177dC2609781EE8A449E1", 0L, (byte)1, 2, 1, 0, 0, 0, 0, 2L, null, 6712105348L, false, 0L, 0, 0, 0, 0, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3945110278L },
                    { 5L, "0xA8f88F9051c444310A71F791f04ac38d12B82B6A", 0L, (byte)1, 2, 1, 0, 0, 0, 0, 2L, null, 6712105348L, false, 0L, 0, 0, 0, 0, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8537692104L },
                    { 6L, "0xAD8EB429B12aCBa6daeAB237278fAE90F231feDD", 0L, (byte)1, 5, 2, 1, 0, 0, 0, 5L, null, 8537692104L, false, 0L, 0, 0, 0, 0, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7261081493L },
                    { 7L, "0x1D9cDdC3F51DBD880b4B1D2F1f882b3EB13fc81f", 0L, (byte)1, 6, 5, 2, 1, 0, 0, 6L, null, 7261081493L, false, 0L, 0, 0, 0, 0, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3749862658L },
                    { 8L, "0x23c4adA813a80EdFCdfE7d8f7Aa95F5a4Ce98FBb", 0L, (byte)1, 6, 5, 2, 1, 0, 0, 6L, null, 7261081493L, false, 0L, 0, 0, 0, 0, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6195102873L },
                    { 9L, "0xEFBb333b1F19b7F953fac9F4165b827191586284", 0L, (byte)1, 6, 5, 2, 1, 0, 0, 6L, null, 7261081493L, false, 0L, 0, 0, 0, 0, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9152106487L },
                    { 10L, "0x4475a92cA5C8Aa534A5fb811Baa7dB943CFe98D0", 0L, (byte)1, 3, 1, 0, 0, 0, 0, 3L, null, 1657410923L, false, 0L, 0, 0, 0, 0, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4710361958L },
                    { 11L, "0x62e024JH89334K0De770FF24524b99FJ9F348J43", 0L, (byte)1, 10, 3, 1, 0, 0, 0, 10L, null, 4710361958L, false, 0L, 0, 0, 0, 0, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9897761958L }
                });

            migrationBuilder.InsertData(
                table: "level",
                columns: new[] { "Id", "FPH_1", "FPH_2", "FPH_3", "FPH_4", "FPH_5", "FPH_6", "FPS_1", "FPS_2", "FPS_3", "FPS_4", "FPS_5", "FPS_6", "LM_1", "LM_2", "LM_3", "LM_4", "LM_5", "LM_6", "LN_1", "LN_2", "LN_3", "LN_4", "LN_5", "LN_6", "LP_1", "LP_2", "LP_3", "LP_4", "LP_5", "LP_6", "LU_1", "LU_2", "LU_3", "LU_4", "LU_5", "LU_6", "MINS", "PLOC", "POOLMIN", "PRPP", "RLOC", "RPH", "RPS" },
                values: new object[] { 1L, 0.0000062499999999999999999600m, 0.0000050000000000000000000400m, 0.0000037500000000000000001200m, 0.0000024999999999999999998400m, 0.0000012499999999999999999200m, 0.0000006249999999999999999600m, 0.0000000017361111111111111111m, 0.0000000013888888888888888889m, 0.0000000010416666666666666667m, 0.0000000006944444444444444444m, 0.0000000003472222222222222222m, 0.0000000001736111111111111111m, 50000m, 200000m, 500000m, 1000000m, 2000000m, 3000000m, "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", 5m, 4m, 3m, 2m, 1m, 0.5m, 125m, 500m, 1250m, 2500m, 5000m, 10000m, 10000m, 0.0020833333333333333333333333m, 300000m, 0.0000002604166666666666668000m, 0.15m, 0.0001249999999999999999999200m, 0.0000000347222222222222222222m });

            migrationBuilder.InsertData(
                table: "stats",
                columns: new[] { "Id", "Daily_LR", "Daily_LRMK", "Daily_NR", "Daily_NRMK", "Hourly_LR", "Hourly_LRMK", "Hourly_NR", "Hourly_NRMK", "TknPrc", "TknSupply", "WdrwFee", "WdrwMin" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0ul, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0ul, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0ul, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0ul, 0.0005m, 10000000000m, 10.0m, 1.0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "acct");

            migrationBuilder.DropTable(
                name: "evt_approval");

            migrationBuilder.DropTable(
                name: "evt_claimed");

            migrationBuilder.DropTable(
                name: "evt_purchased");

            migrationBuilder.DropTable(
                name: "evt_staked");

            migrationBuilder.DropTable(
                name: "evt_transfer");

            migrationBuilder.DropTable(
                name: "evt_unstaked");

            migrationBuilder.DropTable(
                name: "level");

            migrationBuilder.DropTable(
                name: "pro_claimed");

            migrationBuilder.DropTable(
                name: "pro_poolcom");

            migrationBuilder.DropTable(
                name: "pro_refcom");

            migrationBuilder.DropTable(
                name: "pro_staked");

            migrationBuilder.DropTable(
                name: "pro_unstaked");

            migrationBuilder.DropTable(
                name: "stats");
        }
    }
}
