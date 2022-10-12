using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using TOKENAPI.Common;
using TOKENAPI.CQRS;
using TOKENAPI.Domain;
using TOKENAPI.DTO;
using TOKENAPI.EF;
using TOKENAPI.Enums;
using TOKENAPI.Filters;
using TOKENAPI.Mapper;
using TOKENAPI.Models;
using TOKENAPI.Service;

namespace TOKENAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private IUserService _userService;
        private readonly DbCon _dbcon;
        private readonly IMapper _mapper; //= new MapperConfiguration(cfg => cfg.AddProfile(new UsrProfile())).CreateMapper();


        public UserController(IUserService userService, IMapper mapper, DbCon dbcon)
        {
            _userService = userService;
            _mapper = mapper;
            _dbcon = dbcon;
        }



        [HttpPost("register")]
        public async Task<ActionResult> register([FromServices] Stgs stgs, [FromBody] RegUser cmd)
        {

            try
            {
                cmd.MaxLev = stgs.MaxLev;
                var item = await _userService.RegUser(cmd);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(new FBError() { Code = "0", Message = ex.Message, InnerMsg = ex.InnerException?.Message });
            }

        }


        [HttpGet("get/{addr}")]
        [AddrFilter]
        public async Task<ActionResult> get([FromRoute] string addr)
        {
            try
            {
                var item = await _userService.GetUser(addr);
                return Ok(item);
            }catch (Exception ex)
            {
                return Ok("no_acct");
            }

        }

        [HttpGet("comms/{addr}")]
        [AddrFilter]
        public async Task<ActionResult> coms([FromRoute] string addr)
        {
            try
            {
                var item = await _userService.GetComms(addr);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return Ok("no_acct");
            }
        }

        [HttpGet("histrewards/{addr}")]
        [AddrFilter]
        public async Task<ActionResult> histrwds([FromRoute] string addr, [FromQuery] GetHist @p)
        {

            try
            {
                var user = await _userService.GetUser(addr);
                var sql = $"SELECT #*# FROM (" +
                 $"SELECT 'refcom' as Type, x1.* FROM pro_refcom x1  WHERE user = '{addr}' UNION " +
                 $"SELECT 'poolcom' as Type, x2.* FROM pro_poolcom x2 WHERE user = '{addr}') t1 ";

                //var sql = "SELECT # t1.*, TRIM(t2.MarketCap)+0 'MarketCap', TRIM(t2.ATH)+0 'ATH', " +
                //          " TRIM(t2.Prc1HChg)+0 'Prc1HChg',TRIM(t2.Prc1HPct)+0 'Prc1HPct',TRIM(t2.Vol1HChg)+0 'Vol1HChg',TRIM(t2.Vol1HPct)+0 'Vol1HPct',TRIM(t2.MCap1HChg)+0 'MCap1HChg',TRIM(t2.MCap1HPct)+0 'MCap1HPct', " +
                //          " TRIM(t2.Prc1DChg)+0 'Prc1DChg',TRIM(t2.Prc1DPct)+0 'Prc1DPct',TRIM(t2.Vol1DChg)+0 'Vol1DChg',TRIM(t2.Vol1DPct)+0 'Vol1DPct',TRIM(t2.MCap1DChg)+0 'MCap1DChg',TRIM(t2.MCap1DPct)+0 'MCap1DPct', " +
                //          " TRIM(t2.Prc7DChg)+0 'Prc7DChg',TRIM(t2.Prc7DPct)+0 'Prc7DPct',TRIM(t2.Vol7DChg)+0 'Vol7DChg',TRIM(t2.Vol7DPct)+0 'Vol7DPct',TRIM(t2.MCap7DChg)+0 'MCap7DChg',TRIM(t2.MCap7DPct)+0 'MCap7DPct', " +
                //          " TRIM(t2.Prc30DChg)+0 'Prc30DChg',TRIM(t2.Prc30DPct)+0 'Prc30DPct',TRIM(t2.Vol30DChg)+0 'Vol30DChg',TRIM(t2.Vol30DPct)+0 'Vol30DPct',TRIM(t2.MCap30DChg)+0 'MCap30DChg',TRIM(t2.MCap30DPct)+0 'MCap30DPct', " +
                //          " TRIM(t2.PrcYtdChg)+0 'PrcYtdChg',TRIM(t2.PrcYtdPct)+0 'PrcYtdPct',TRIM(t2.VolYtdChg)+0 'VolYtdChg',TRIM(t2.VolYtdPct)+0 'VolYtdPct',TRIM(t2.MCapYtdChg)+0 'MCapYtdChg',TRIM(t2.MCapYtdPct)+0 'MCapYtdPct' " +
                //          $"# FROM {Tbl.CoinFolio} t1 join {Tbl.Nomics} t2 ON t1.Coin=t2.Symbol WHERE t1.UId={Usr.UId} {where} ";
                var curCol = await _dbcon.GetList<RwdDto>(sql, p);

                if (curCol is null || curCol.Result is null)
                    return Ok(new FBCollection<RwdDto>(default));

                var dto = _mapper.Map<IEnumerable<RwdDto>>(curCol.Result);
                var newCol = new FBCollection<RwdDto>(dto, @p.PageNo, @p.PageSize, curCol.QueryCount);
                return Ok(newCol);
            }
            catch (Exception ex)
            {
                return BadRequest(new FBError() { Code = "0", Message = ex.Message, InnerMsg = ex.InnerException?.Message });
            }

        }

        [HttpGet("histstaking/{addr}")]
        [AddrFilter]
        public async Task<ActionResult> histstks([FromRoute] string addr, [FromQuery] GetHist @p)
        {
            try
            {
                var user = await _userService.GetUser(addr);
                var sql = $"SELECT #*# FROM (" +
                 $"SELECT 'staked' as Type, x1.* FROM evt_staked x1  WHERE user = '{addr}' UNION " +
                 $"SELECT 'claimed' as Type, x2.* FROM evt_claimed x2 WHERE user = '{addr}' UNION " +
                 $"SELECT 'unstaked' as Type, x3.* FROM evt_unstaked x3 WHERE user = '{addr}') t1 ";
                var curCol = await _dbcon.GetList<StkDto>(sql, p);

                if (curCol is null || curCol.Result is null)
                    return Ok(new FBCollection<StkDto>(default));

                var dto = _mapper.Map<IEnumerable<StkDto>>(curCol.Result);
                var newCol = new FBCollection<StkDto>(dto, @p.PageNo, @p.PageSize, curCol.QueryCount);
                return Ok(newCol);
            }
            catch (Exception ex)
            {
                return BadRequest(new FBError() { Code = "0", Message = ex.Message, InnerMsg = ex.InnerException?.Message });
            }

        }


        [HttpGet("refs/{addr}")]
        [AddrFilter]
        public async Task<ActionResult> refs([FromRoute] string addr, [FromQuery] GetRefs @p)
        {

            try
            {

                if (@p.Level == 0) throw new FBException("", "Level cannot be 0");
                var user = await _userService.GetUser(addr);
                @p.RefId = user.Id;

                var curCol = await _userService.GetRefs(@p);

                if (curCol is null || curCol.Result is null)
                    return Ok(new FBCollection<RefsDto>(default));

                var dto = _mapper.Map<IEnumerable<RefsDto>>(curCol.Result);
                var newCol = new FBCollection<RefsDto>(dto, @p.PageNo, @p.PageSize, curCol.QueryCount);
                return Ok(newCol);
            }
            catch (Exception ex)
            {
                return BadRequest(new FBError() { Code = "0", Message = ex.Message, InnerMsg = ex.InnerException?.Message });
            }

        }




        [HttpGet("jobruns")]
        public async Task<ActionResult> balance()
        {

            var lr = await _dbcon.SqlToEnt<object>($"select CURRENT_TIMESTAMP, Hourly_LR '10m_LastRun', Hourly_NR '10m_NextRun', Daily_LR '30m_LastRun', Daily_NR '30m_NextRun' from stats");
            return Ok(lr);
        }


        [HttpGet("txid/{txid}")]
        public async Task<ActionResult> GetTxid([FromRoute] string txid, [FromQuery] Event evt)
        {
            string sql = "";
            if(evt == Event.Stake)
            {
                sql = $"SELECT t1.* FROM evt_staked t1 WHERE txid LIKE '%{txid}%'";

            }else if(evt == Event.Unstake)
            {
                sql = $"SELECT t1.* FROM evt_unstaked t1 WHERE txid LIKE '%{txid}%'";

            }
            var dat = await _dbcon.SqlToEnt<EvtDto>(sql);
            return Ok(dat);
        }



    }
}
