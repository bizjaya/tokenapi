using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using Utf8Json;
using TOKENAPI.Common;
using TOKENAPI.Models;
using Nethereum.Web3;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;

namespace TOKENAPI.Service
{
    public interface ICont
    {
        Task<string> ContAddr(string? contName,int netId);
        Task<string> ContAbi(string? contName);
        Task<string> ContBCode(string? contName);
        Task<ulong> LastBlock();
        Task<ulong> UpdateBlock(ulong block);
        Task<(Web3 web3, Contract contract, ulong LastBlock, ulong LatestBlock)> GetCont(bool key=false);
        int MaxLev { get; }
        Stgs stgs { get; }
        ToBN ToBN(decimal amount, decimal fees);

    }
    public class Cont: ICont
    {

        private readonly IMemoryCache _memcache;
        private readonly Stgs _stgs;


        private string ContractPath(string contName)
        {
            string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), _stgs.ContDir));
            string contractPath = Path.Combine(filePath, contName);
            return contractPath;
        }

        public Cont(IMemoryCache memcache, Stgs stgs)
        {
            _memcache = memcache;
            _stgs = stgs;
        }

        public int MaxLev { get => _stgs.MaxLev;  }
        public Stgs stgs { get => _stgs; }



        public async Task<(Web3 web3, Contract contract, ulong LastBlock, ulong LatestBlock)> GetCont(bool key = false)
        {
            Web3 web3 = (!key)? new Web3(_stgs.Network): new Web3(new Account(_stgs.PriKey, _stgs.NetId),_stgs.Network);
            web3.TransactionManager.UseLegacyAsDefault = true;

            var contract = web3.Eth.GetContract(await ContAbi(_stgs.ContName), await ContAddr(_stgs.ContName, _stgs.NetId));
            ulong lastBlock = await LastBlock();
            var latestBlock = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();

            return (web3, contract, lastBlock, latestBlock.ToUlong());
        }


        public async Task<string> ContAbi(string? contName)
        {
            if (!_memcache.TryGetValue(Const.ContAbi, out string contAbi))
            {
                using (var file = File.OpenText(ContractPath(contName ?? "")))
                {
                    string s = await file.ReadToEndAsync();
                    var json = JsonSerializer.Deserialize<dynamic>(s);
                    contAbi = JsonSerializer.ToJsonString(json["abi"]);

                    _memcache.Set(Const.ContAbi, contAbi, Const.MemOpt);
                }
            }
            return contAbi;
        }

        public async Task<string> ContBCode(string? contName)
        {
            if (!_memcache.TryGetValue(Const.ContBCode, out string contBCode))
            {
                using (var file = File.OpenText(ContractPath(contName ?? "")))
                {
                    string s = await file.ReadToEndAsync();
                    var json = JsonSerializer.Deserialize<dynamic>(s);
                    contBCode = JsonSerializer.ToJsonString(json["bytecode"]).Replace("\"","");

                    _memcache.Set(Const.ContBCode, contBCode, Const.MemOpt);
                }
            }
            return contBCode;
        }


        public async Task<string> ContAddr(string? contName, int netId)
        {
            if (!_memcache.TryGetValue(Const.ContAddr, out string contAddr))
            {
                using (var file = File.OpenText(ContractPath(contName??"")))
                {
                    string s = await file.ReadToEndAsync();
                    var json = JsonSerializer.Deserialize<dynamic>(s);
                    contAddr = JsonSerializer.ToJsonString(json["networks"][netId.ToString()]["address"]).Replace("\"","");

                    //string pat = "\"address\": \"([^\"]*)\",";
                    ////Match m = Regex.Match(s, pat, RegexOptions.IgnoreCase);
                    //var m = Regex.Match(s, pat, RegexOptions.IgnoreCase).Groups;
                    //contAddr = m[1].Value;
                    _memcache.Set(Const.ContAddr, contAddr, Const.MemOpt);                                   
                }
            }
            return contAddr;
        }


        public Task<ulong> LastBlock()
        {
            if (!_memcache.TryGetValue(Const.PrevBlock, out ulong prevBlock))
            {
                prevBlock = 1;
                _memcache.Set(Const.PrevBlock, prevBlock, Const.MemOpt);            
            }
            return Task.FromResult(prevBlock);
        }

        public Task<ulong> UpdateBlock(ulong block)
        {
            var dat = _memcache.Set(Const.PrevBlock, block, Const.MemOpt);
            if (!_memcache.TryGetValue(Const.PrevBlock, out ulong prevBlock))
            {
                prevBlock = block;
            }
            return Task.FromResult(prevBlock);


        }

        public ToBN ToBN(decimal amount, decimal fees)
        {
            BigInteger bn1 = new BigInteger((double)amount * Math.Pow(10, _stgs.Decimals));
            BigInteger fee = new BigInteger((double)bn1 * (double)(fees * 0.01m));

            BigInteger bal = bn1 - fee;

            return new ToBN
            {
                Amount = bn1,
                Fees = fee,
                ToPay = bal,
                paid = bn1.FrWei(_stgs.Decimals),
                fees = fee.FrWei(_stgs.Decimals),
            };
        }
    }
}
