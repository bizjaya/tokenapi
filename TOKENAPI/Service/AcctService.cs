using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Nethereum.Contracts;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.Model;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using TOKENAPI.BG;
using TOKENAPI.Common;
using TOKENAPI.CQRS;
using TOKENAPI.Domain;
using TOKENAPI.DTO;
using TOKENAPI.EF;
using TOKENAPI.Enums;
using TOKENAPI.Events;
using TOKENAPI.Mapper;
using TOKENAPI.Models;

namespace TOKENAPI.Service
{
    public interface IAcctService
    {

        Task Every3M();
        Task Every1H();
        Task Every10M();


        Task GetBalance();
        Task GetEvents(ulong? prevBlock=null);
        Task ProEvents();
        Task ProRefRwd();
        Task ProPoolRwd();
        Task<Contract> ContDetails();

        Task<UserDto> TrigStake(string addr, ulong? blockno=null);
        Task<UserDto> TrigUnstake(string addr, ulong? blockno = null);
        Task<UserDto> TrigClaim(string addr);
        Task<UserDto> ClaimRefRwd(ClaimRwd cmd);
        Task<UserDto> ClaimPoolRwd(ClaimRwd cmd);
        Task XtkPolling();

    }

    public class AcctService : IAcctService
    {
        //private Web3 web3;

        private readonly ICont _cont;
        //private readonly Stgs _stgs;

        //private readonly IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new EvtProfile())).CreateMapper();
        private readonly IMapper _mapper;
        private readonly IDbContextFactory<DbCtx> _factory;
        private readonly DbCon _dbcon;
        private readonly IBGTaskQueue _queue;
       
        public AcctService(ICont cont, IDbContextFactory<DbCtx> factory, DbCon dbcon, IMapper mapper, IBGTaskQueue queue)
        {
            _cont = cont;
            //_stgs = stgs;
            _factory = factory;
            _dbcon = dbcon;
            _mapper = mapper;
            _queue = queue;
        }

        public async Task Every3M()
        {
            await GetEvents(null);
            await ProEvents();
        }

        public async Task Every1H()
        {
            await ProRefRwd();
            await ProPoolRwd();
        }

        public async Task Every10M()
        {
            await XtkPolling();
        }



        public async Task GetBalance()
        {

            (Web3 web3, Contract contract, ulong prevBlock, ulong latestBlock) = await _cont.GetCont();
            var balance = await web3.Eth.GetBalance.SendRequestAsync("0x8fb5BEf92cd481eCAcc87fEA4aa9470b47d19972");
        }

        public async Task GetEvents(ulong? lastBlock=null)
        {

            (Web3 web3, Contract contract, ulong prevBlock, ulong latestBlock) = await _cont.GetCont();
            lastBlock ??= prevBlock;

            lastBlock = (latestBlock - lastBlock > 4000) ? (latestBlock -4000) : lastBlock;

            if (lastBlock.ToString() == latestBlock.ToString()) return;

            var transferEvent = contract.GetEvent(Const.Transfer);
            var transferInput = transferEvent.CreateFilterInput(new BlockParameter((ulong)lastBlock), new BlockParameter(latestBlock));
            var transferData = (await transferEvent.GetAllChangesAsync<TransferEvt>(transferInput)).Select(x =>  new { e = x.Event, l = x.Log }).ToList();
            transferData.ForEach( x => { x.e.blockno = x.l.BlockNumber; x.e.txid = x.l.TransactionHash; });
            var transfer = _mapper.Map<List<EvtTransfer>>(transferData.Select(x=>x.e));



            var stakedEvent = contract.GetEvent(Const.Staked);
            var stakedInput = stakedEvent.CreateFilterInput(new BlockParameter((ulong)lastBlock), new BlockParameter(latestBlock));
            var stakedData = (await stakedEvent.GetAllChangesAsync<StakedEvt>(stakedInput)).Select(x => new { e = x.Event, l = x.Log }).ToList();
            stakedData.ForEach(x => { x.e.blockno = x.l.BlockNumber; x.e.txid = x.l.TransactionHash; });
            var staked = _mapper.Map<List<EvtStaked>>(stakedData.Select(x => x.e));



            var unstakedEvent = contract.GetEvent(Const.Unstaked);
            var unstakedInput = unstakedEvent.CreateFilterInput(new BlockParameter((ulong)lastBlock), new BlockParameter(latestBlock));
            var unstakedData = (await unstakedEvent.GetAllChangesAsync<UnstakedEvt>(unstakedInput)).Select(x => new { e = x.Event, l = x.Log }).ToList();
            unstakedData.ForEach(x => { x.e.blockno = x.l.BlockNumber; x.e.txid = x.l.TransactionHash; });
            var unstaked = _mapper.Map<List<EvtUnstaked>>(unstakedData.Select(x => x.e));



            var claimedEvent = contract.GetEvent(Const.Claimed);
            var claimedInput = claimedEvent.CreateFilterInput(new BlockParameter((ulong)lastBlock), new BlockParameter(latestBlock));
            var claimedData = (await claimedEvent.GetAllChangesAsync<ClaimedEvt>(claimedInput)).Select(x => new { e = x.Event, l = x.Log }).ToList();
            claimedData.ForEach(x => { x.e.blockno = x.l.BlockNumber; x.e.txid = x.l.TransactionHash; });
            var claimed = _mapper.Map<List<EvtClaimed>>(claimedData.Select(x => x.e));



            using (var DbCtx = _factory.CreateDbContext())
            {
                var uow = new UnitOfWork(DbCtx);

                var uniEvtTransfer = transfer.Where( x =>  !DbCtx.EvtTransfer.Any(y => y.txid == x.txid)).ToList();
                var uniEvtStaked = staked.Where(x => !DbCtx.EvtStaked.Any(y => y.txid == x.txid)).ToList();
                var uniUnstaked = unstaked.Where(x => !DbCtx.EvtUnstaked.Any(y => y.txid == x.txid)).ToList();
                var uniClaimed = claimed.Where(x => !DbCtx.EvtClaimed.Any(y => y.txid == x.txid)).ToList();


                await uow.EvtTransferRepo.AddRangeAsync(uniEvtTransfer);
                await uow.EvtStakedRepo.AddRangeAsync(uniEvtStaked);
                await uow.EvtUnstakedRepo.AddRangeAsync(uniUnstaked);
                await uow.EvtClaimedRepo.AddRangeAsync(uniClaimed);

                await uow.CommitAsync();
            }

            await _cont.UpdateBlock( latestBlock);

        }

        public async Task<UserDto> _spGetUser(long? uid = null, string? addr = null) => await _dbcon.SPROC<UserDto>("userget", new { uid = uid, addr = addr });

        public async Task<UserDto> TrigStake(string addr, ulong? blockno=null)
        {
            (Web3 web3, Contract contract, ulong prevBlock, ulong latestBlock) = await _cont.GetCont();
            if (blockno != null) prevBlock = latestBlock = blockno ?? 0;

            var stakedEvent = contract.GetEvent(Const.Staked);
            var stakedInput = stakedEvent.CreateFilterInput(new BlockParameter(prevBlock), new BlockParameter(latestBlock));
            var stakedData = (await stakedEvent.GetAllChangesAsync<StakedEvt>(stakedInput)).Select(x => new { e = x.Event, l = x.Log }).ToList();
            stakedData.ForEach(x => { x.e.blockno = x.l.BlockNumber; x.e.txid = x.l.TransactionHash; });
            var staked = _mapper.Map<List<EvtStaked>>(stakedData.Select(x => x.e));

            var evt = staked.OrderByDescending(x=>x.timestamp).FirstOrDefault(x => x.user.EqualI(addr));
            var cnt = await _dbcon.Count($"SELECT COUNT(Id) FROM {Const.TblProStaked} WHERE user='{addr}' AND txid='{evt?.txid}';");
            if (evt != null && cnt==0)
            {
                var spstr = $"CALL stakeadd('{evt.amount_}', '{evt.total_}', '{evt.unamt_}', '{evt.timestamp}', '{evt.user}',  '{evt.txid}', '{evt.blockno}', '{_cont.MaxLev}')";
                var data = await _dbcon.SPROC<SPReturn>("stakeadd", new stkprm { amt = evt.amount_, ttl = evt.total_, unamt = evt.unamt_, stamp = evt.timestamp, ad_dr = addr, tx_id = evt.txid, b_no = evt.blockno, m_lev = _cont.MaxLev });
            }
            return await _spGetUser(addr: addr);
            
        }

        public async Task<UserDto> TrigUnstake(string addr, ulong? blockno = null)
        {
            (Web3 web3, Contract contract, ulong prevBlock, ulong latestBlock) = await _cont.GetCont();
            if (blockno != null) prevBlock = latestBlock = blockno??0;
            
            var unstakedEvent = contract.GetEvent(Const.Unstaked);
            var unstakedInput = unstakedEvent.CreateFilterInput(new BlockParameter((ulong)prevBlock), new BlockParameter(latestBlock));
            var unstakedData = (await unstakedEvent.GetAllChangesAsync<UnstakedEvt>(unstakedInput)).Select(x => new { e = x.Event, l = x.Log }).ToList();
            unstakedData.ForEach(x => { x.e.blockno = x.l.BlockNumber; x.e.txid = x.l.TransactionHash; });
            var unstaked = _mapper.Map<List<EvtUnstaked>>(unstakedData.Select(x => x.e));

            var evt = unstaked.OrderByDescending(x => x.timestamp).FirstOrDefault(x => x.user.EqualI(addr));
            var cnt = await _dbcon.Count($"SELECT COUNT(Id) FROM {Const.TblProUnstaked} WHERE user='{addr}' AND txid='{evt?.txid}';");
            if (evt != null && cnt==0)
            {
                var spstr = $"CALL stakerem('{evt.amount_}', '{evt.total_}', '{evt.unamt_}', '{evt.timestamp}', '{evt.user}',  '{evt.txid}', '{evt.blockno}', '{_cont.MaxLev}')";
                var data = await _dbcon.SPROC<SPReturn>("stakerem", new stkprm { amt = evt.amount_, ttl = evt.total_, unamt = evt.unamt_, stamp = evt.timestamp, ad_dr = addr, tx_id = evt.txid, b_no = evt.blockno, m_lev = _cont.MaxLev });
            }
            return await _spGetUser(addr: addr);
       
        }


        public async Task<UserDto> TrigClaim(string addr)
        {
            (Web3 web3, Contract contract, ulong prevBlock, ulong latestBlock) = await _cont.GetCont();
            var claimedEvent = contract.GetEvent(Const.Staked);
            var claimedInput = claimedEvent.CreateFilterInput(new BlockParameter((ulong)prevBlock), new BlockParameter(latestBlock));
            var claimedData = (await claimedEvent.GetAllChangesAsync<ClaimedEvt>(claimedInput)).Select(x => new { e = x.Event, l = x.Log }).ToList();
            claimedData.ForEach(x => { x.e.blockno = x.l.BlockNumber; x.e.txid = x.l.TransactionHash; });
            var claimed = _mapper.Map<List<EvtClaimed>>(claimedData.Select(x => x.e));

            var evt = claimed.OrderByDescending(x => x.timestamp).FirstOrDefault(x => x.user.EqualI(addr));
            var cnt = await _dbcon.Count($"SELECT COUNT(Id) FROM {Const.TblProClaimed} WHERE user='{addr}' AND txid='{evt?.txid}';");
            if (evt != null && cnt==0)
            {
                var data = await _dbcon.SPROC<SPReturn>("claimrwd", new { ad_dr = addr, amt= evt.amount_ });
            }
            return await _spGetUser(addr: addr);
            
        }

        public async Task ProEvents()
        {
            var evtStaked = await _dbcon.SPROCLIST<EvtStaked>("unproc", new { type="stk" }); //unprocessed staked events
            foreach(var evt in evtStaked)
            {

                var spstr = $"CALL stakeadd({evt.amount_}, {evt.total_}, {evt.unamt_}, {evt.timestamp}, '{evt.user}',  '{evt.txid}', '{evt.blockno}', {_cont.MaxLev})";
                _queue.QueueBGWork(async token =>{
                        var data = await _dbcon.SPROC<SPReturn>("stakeadd", new stkprm { amt = evt.amount_, ttl = evt.total_, unamt = evt.unamt_, stamp = evt.timestamp, ad_dr = evt.user, tx_id = evt.txid, b_no = evt.blockno, m_lev = _cont.MaxLev });
                              
                });
            }

            var evtUnstaked = await _dbcon.SPROCLIST<EvtUnstaked>("unproc", new { type = "utk" }); //unprocessed staked events
            foreach (var evt in evtUnstaked)
            {
                var spstr = $"CALL stakerem({evt.amount_}, {evt.total_}, {evt.unamt_}, {evt.timestamp}, '{evt.user}',  '{evt.txid}', '{evt.blockno}', {_cont.MaxLev})";
                _queue.QueueBGWork(async token => {
                    var data = await _dbcon.SPROC<SPReturn>("stakerem", new stkprm { amt = evt.amount_, ttl = evt.total_, unamt = evt.unamt_, stamp = evt.timestamp, ad_dr = evt.user, tx_id = evt.txid, b_no = evt.blockno, m_lev = _cont.MaxLev });
                });
            }


            var evtClaimed = await _dbcon.SPROCLIST<EvtClaimed>("unproc", new { type = "clm" }); //reward claimed events
            foreach (var evt in evtClaimed)
            {
                var spstr = $"CALL claimrwd('{evt.user}')";

                _queue.QueueBGWork(async token => {
                    var data = await _dbcon.SPROC<SPReturn>("claimrwd", new { ad_dr = evt.user, amt = evt.amount_ });
                });
            }
        }

        public async Task ProRefRwd()
        {
            var (top_dt, top_mk, end_dt, end_mk) = FBHelper.GetTimeFrame(TUnit.MINUTE, 10, 10);
           var data = await _dbcon.SqlToEnt<StatDto>($"SELECT * FROM {Const.TblStats} WHERE Id=1");
            if (data.Hourly_NRMK!=0 && data.Hourly_LRMK < top_mk) // now mk > next runmk.
            {
                await _dbcon.SPROC<SPReturn>("rewards");
            }
            await _dbcon.Execute($"UPDATE {Const.TblStats} SET Hourly_LR='{top_dt}',Hourly_LRMK='{top_mk}',Hourly_NR='{end_dt}',Hourly_NRMK='{end_mk}' WHERE Id=1");
        }

        public async Task ProPoolRwd()
        {

            var( top_dt,top_mk, end_dt, end_mk) = FBHelper.GetTimeFrame(TUnit.MINUTE, 10, 30);

            var data = await _dbcon.SqlToEnt<StatDto>($"SELECT * FROM {Const.TblStats} WHERE Id=1");
            if (data.Daily_NRMK != 0 && data.Daily_LRMK < top_mk) // now mk > next runmk.
            {
                await _dbcon.SPROC<SPReturn>("poolrwds");
            }
            await _dbcon.Execute($"UPDATE {Const.TblStats} SET Daily_LR='{top_dt}',Daily_LRMK='{top_mk}',Daily_NR='{end_dt}',Daily_NRMK='{end_mk}' WHERE Id=1");
        }

        public async Task<Contract> ContDetails()
        {
            (_, Contract contract, _, _) = await _cont.GetCont();

            return contract;

        }

        private async Task<bool> RwdBalChk(Web3 web3, Contract contract, BigInteger amt)
        {
            var balance = await web3.Eth.GetBalance.SendRequestAsync(_cont.stgs.FrAddr);
            var ethBal = Web3.Convert.FromWei(balance.Value);

            object[] @params = new object[1] { _cont.stgs.FrAddr };
            var balOf = contract.GetFunction("balanceOf");
            var balRes = await balOf.CallAsync<BigInteger>(@params);

            double tokBal = balRes.FrWeiDbl(_cont.stgs.Decimals);
            double amount = amt.FrWeiDbl(_cont.stgs.Decimals);

            if (tokBal < amount || tokBal < 100000d || ethBal < 0.1m)
            {
                return false;
            }
            return true;
        }

        public async Task<UserDto> ClaimRefRwd(ClaimRwd cmd)
        {

            try
            {
                var user = await _spGetUser(addr: cmd.User);
                if (user == null) throw new FBException("", "Account Doesn't Exist");
                if (user.RefUnc<cmd.Amount || cmd.Amount<=0) throw new FBException("", "Invalid Amount");

                var stats = await _dbcon.SqlToEnt<Statz>($"SELECT TknPrc,WdrwFee,WdrwMin FROM {Const.TblStats} WHERE Id='1'");
                //      if(stats.TknPrc * cmd.Amount < stats.WdrwMin) throw new FBException("", $"Minimum Amount is  ${stats.WdrwMin}");
                ToBN BN = _cont.ToBN(cmd.Amount, stats.WdrwFee);

     
                decimal minDraw = stats.WdrwMin / stats.TknPrc;
                if(BN.ToPay <= 0 || cmd.Amount < minDraw) throw new FBException("", $"Minimum is {Math.Ceiling(minDraw)}");

                (Web3 web3, Contract contract, _, _) = await _cont.GetCont(true);
                var function = contract.GetFunction("claimref");

                var balChk = await RwdBalChk(web3, contract, BN.ToPay);
                if (!balChk) throw new FBException("", "Try Again later!");


                string addr = cmd.User;

                object[] @params = new object[3]{ addr, BN.ToPay, BN.Fees };

                //var transactionManager = web3.TransactionManager;
                var txinput = new CallInput()
                {
                    From =_cont.stgs.FrAddr,
                    To= cmd.User,
                    Data = await _cont.ContBCode(_cont.stgs.ContName) // remove due to reverted eth_gasEstimate issue
                };
              //  var gasEstimate = await transactionManager.EstimateGasAsync(txinput);

                var gas = await web3.Eth.Transactions.EstimateGas.SendRequestAsync(txinput); //new CallInput(await _cont.ContBCode(_cont.stgs.ContName), _cont.stgs.FrAddr));
                var gasPrice = await web3.Eth.GasPrice.SendRequestAsync();
                var nugas = new HexBigInteger(gas + Web3.Convert.ToWei(1, UnitConversion.EthUnit.Kwei));

             //   var defgas = new HexBigInteger(Web3.Convert.ToWei(0.01, UnitConversion.EthUnit.Gwei));
               // var nugas = new HexBigInteger(gasEstimate.Value + Web3.Convert.ToWei(1, UnitConversion.EthUnit.Gwei));

                // var txHash = await function.CallAsync<object> (_cont.stgs.FrAddr.ToString(), new HexBigInteger(30000000000), new HexBigInteger(0), @params);
                var txHash = await function.SendTransactionAsync(_cont.stgs.FrAddr.ToString(), nugas, new HexBigInteger(0), @params);

                if (txHash != null)
                {
                    var spstr = $"CALL claimref('{cmd.User}', '{cmd.Amount}, '{BN.paid}', '{BN.fees}', '{txHash}')";
                    await _dbcon.SPROC<SPReturn>("claimref", new{ ad_dr = cmd.User, amt = cmd.Amount, pa_id=BN.paid, fe_es = BN.fees, tx_id = txHash });

                    //var data = await _dbcon.SPROC<SPReturn>("claimrwd", new { amt = cmd.Amount, u_id = acct.Id });
                    return await _spGetUser(uid: user.Id);
                }
                else throw new FBException("", "Error while sending!");

            }catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<UserDto> ClaimPoolRwd(ClaimRwd cmd)
        {

            try
            {

                var user = await _spGetUser(addr: cmd.User);
                if (user == null) throw new FBException("", "Account Doesn't Exist");
                if (user.PoolUnc < cmd.Amount || cmd.Amount <= 0) throw new FBException("", "Invalid Amount");

                var stats = await _dbcon.SqlToEnt<Statz>($"SELECT TknPrc,WdrwFee,WdrwMin FROM {Const.TblStats} WHERE Id='1'");
                //      if(stats.TknPrc * cmd.Amount < stats.WdrwMin) throw new FBException("", $"Minimum Amount is  ${stats.WdrwMin}");

                ToBN BN = _cont.ToBN(cmd.Amount, stats.WdrwFee);

                decimal minDraw = stats.WdrwMin / stats.TknPrc;
                if (BN.ToPay <= 0 || cmd.Amount < minDraw) throw new FBException("", "Insufficent Minimum/Fees");

                (Web3 web3, Contract contract, _, _) = await _cont.GetCont(true);
                var function = contract.GetFunction("claimref");

                var balChk = await RwdBalChk(web3, contract, BN.ToPay);
                if(!balChk) throw new FBException("", "Try Again later!");

                string addr = cmd.User;

                object[] @params = new object[3] { addr, BN.ToPay, BN.Fees };

                var txinput = new CallInput()
                {
                    From = _cont.stgs.FrAddr,
                    To = cmd.User,
                    Data = await _cont.ContBCode(_cont.stgs.ContName) // remove due to reverted eth_gasEstimate issue
                };
                //  var gasEstimate = await transactionManager.EstimateGasAsync(txinput);

                var gas = await web3.Eth.Transactions.EstimateGas.SendRequestAsync(txinput); //new CallInput(await _cont.ContBCode(_cont.stgs.ContName), _cont.stgs.FrAddr));
                var gasPrice = await web3.Eth.GasPrice.SendRequestAsync();
                var nugas = new HexBigInteger(gas + Web3.Convert.ToWei(1, UnitConversion.EthUnit.Kwei));
                // var nugas = new HexBigInteger(gasEstimate.Value + Web3.Convert.ToWei(1, UnitConversion.EthUnit.Gwei));

                // var txHash = await function.CallAsync<object> (_cont.stgs.FrAddr.ToString(), new HexBigInteger(30000000000), new HexBigInteger(0), @params);
                var txHash = await function.SendTransactionAsync(_cont.stgs.FrAddr.ToString(), nugas, new HexBigInteger(0), @params);

                if (txHash != null)
                {
                    var spstr = $"CALL claimpool('{cmd.User}', '{cmd.Amount}, '{BN.paid}', '{BN.fees}', '{txHash}')";

                    await _dbcon.SPROC<SPReturn>("claimpool", new { ad_dr = cmd.User, amt = cmd.Amount, pa_id = BN.paid, fe_es = BN.fees, tx_id = txHash });

                    //var data = await _dbcon.SPROC<SPReturn>("claimrwd", new { amt = cmd.Amount, u_id = acct.Id });
                    return await _spGetUser(uid: user.Id);
                }
                else throw new FBException("", "Error while sending!");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task XtkPolling()
        {
            try
            {
                (Web3 web3, Contract contract, _, _) = await _cont.GetCont(true);

                using (var DbCtx = _factory.CreateDbContext())
                {
                    var uow = new UnitOfWork(DbCtx);
                    object[] @params = new object[1] { _cont.stgs.StkLBack };


                    var stkfunc = contract.GetFunction("getStkCol");
                    var stkres = await stkfunc.CallAsync<List<XtkEvt>>(@params);
                    var stkevt = _mapper.Map<List<EvtXtk>>(stkres);
                    var uniEvtStaked = stkevt
                        .Where(x=>x.bno!=0)
                        .Where(x => !DbCtx.EvtStaked.Any(y => y.blockno == x.bno && y.user == x.addr && y.timestamp == x.ts)).ToList()
                        .Where(x => !DbCtx.ProStaked.Any(y => y.blockno == x.bno && y.user == x.addr && y.amount == (double)x.amt_)).ToList();



                    foreach (var stk in uniEvtStaked)
                    {
                        _queue.QueueBGWork(async token => {
                            await TrigStake(stk.addr, stk.bno);
                        });
                    }

                    var utkfunc = contract.GetFunction("getUtkCol");
                    var utkres = await utkfunc.CallAsync<List<XtkEvt>>(@params);
                    var utkevt = _mapper.Map<List<EvtXtk>>(utkres);
                    var uniEvtUnstaked = utkevt
                        .Where(x => x.bno != 0)
                        .Where(x => !DbCtx.EvtUnstaked.Any(y => y.blockno == x.bno && y.user == x.addr && y.timestamp == x.ts)).ToList()
                        .Where(x => !DbCtx.ProUnstaked.Any(y => y.blockno == x.bno && y.user == x.addr && y.amount == (double)x.amt_)).ToList();

                    foreach (var stk in uniEvtUnstaked)
                    {
                        _queue.QueueBGWork(async token => {
                            await TrigUnstake(stk.addr, stk.bno);
                        });
                    }
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
