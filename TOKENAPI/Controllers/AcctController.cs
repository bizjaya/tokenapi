using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Xml.Linq;
using TOKENAPI.Common;
using TOKENAPI.CQRS;
using TOKENAPI.DTO;
using TOKENAPI.EF;
using TOKENAPI.Filters;
using TOKENAPI.Service;

namespace TOKENAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AcctController : ControllerBase
    {

        private IAcctService _acctService;

        private readonly ILogger<AcctController> _logger;
        private readonly DbCon _dbcon;


        public AcctController(ILogger<AcctController> logger, IAcctService acctService, DbCon dbcon)
        {
            _logger = logger;
            _acctService = acctService;
            _dbcon = dbcon;

        }

        [HttpGet("balance")]
        public async Task<ActionResult> balance()
        {

            await _acctService.GetBalance();
            return Ok();
        }

        [HttpGet("getevents")]
        public async Task<ActionResult> events([FromQuery] ulong? no)
        {

            await _acctService.GetEvents(no);
            return Ok();
        }

        [HttpGet("procontract")]
        public async Task<ActionResult> contract()
        {
            return Ok(await _acctService.ContDetails());
        }

        [HttpGet("proevents")]
        public async Task<ActionResult> procs()
        {

            await _acctService.ProEvents();
            return Ok();
        }

        [HttpGet("prorefrwd")]
        public async Task<ActionResult> refrwd()
        {

            await _acctService.ProRefRwd();
            return Ok();
        }

        [HttpGet("propoolrwd")]
        public async Task<ActionResult> poolrwd()
        {

            await _acctService.ProPoolRwd();
            return Ok();
        }


        [HttpGet("XtkPolling")]
        public async Task<ActionResult> stkcol()
        {
            await _acctService.XtkPolling();
            return Ok();
        }

        private async Task<UserDto> getUser(long? uid = null, string? addr = null) => await _dbcon.SPROC<UserDto>("userget", new { uid = uid, addr = addr });



        [HttpPost("stake")]
        [AddrFilter]
        public async Task<ActionResult> stake([FromBody] AddrPost addr)
        {
            var item = await _acctService.TrigStake(addr.Addr);
            return Ok(item);
        }

        [HttpPost("unstake")]
        [AddrFilter]
        public async Task<ActionResult> unstake([FromBody] AddrPost addr)
        {
            var item = await _acctService.TrigUnstake(addr.Addr);
            return Ok(item);
        }


        [HttpPost("claim")]
        [AddrFilter]
        public async Task<ActionResult> claim([FromBody] AddrPost addr)
        {
            var item = await _acctService.TrigClaim(addr.Addr);
            return Ok(item);
        }



        [HttpPost("clref")]
        [AddrFilter]
        public async Task<ActionResult> claimref([FromBody] AddrPost addr)
        {
            try
            {
                var item = await _acctService.ClaimRefRwd(new ClaimRwd { User = addr.Addr, Amount = addr.Amount ?? 0 });
                return Ok(item);

            }
            catch (Exception ex)
            {
                return BadRequest(new FBError() { Code = "0", Message = ex.Message, InnerMsg = ex.InnerException?.Message });
            }
        }

        [HttpPost("clpool")]
        [AddrFilter]
        public async Task<ActionResult> claimpool([FromBody] AddrPost addr)
        {
            try
            {
                var item = await _acctService.ClaimPoolRwd(new ClaimRwd { User = addr.Addr, Amount = addr.Amount ?? 0 });
                return Ok(item);

            }
            catch (Exception ex)
            {
                return BadRequest(new FBError() { Code = "0", Message = ex.Message, InnerMsg = ex.InnerException?.Message });
            }
        }

    }

}
