using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TOKENAPI.Common;
using TOKENAPI.CQRS;
using TOKENAPI.Domain;
using TOKENAPI.DTO;
using TOKENAPI.EF;
using TOKENAPI.Mapper;
using TOKENAPI.Models;

namespace TOKENAPI.Service
{

    public interface IUserService
    {
        Task<UserDto> RegUser(RegUser cmd);
        Task<UserDto> GetUser(string addr);
        Task<FBQueRes<Acct>> GetRefs(GetRefs cmd);
        Task<CommsDto> GetComms(string addr);


    }
    public class UserService: IUserService
    {

        private readonly IMapper _mapper; // = new MapperConfiguration(cfg => cfg.AddProfile(new UsrProfile())).CreateMapper();
        private readonly IDbContextFactory<DbCtx> _factory;
        private readonly DbCon _dbcon;


        public UserService(IDbContextFactory<DbCtx> factory, DbCon dbcon, IMapper mapper)
        {
            _factory = factory;
            _dbcon = dbcon;
            _mapper = mapper;
        }

        public async Task<UserDto> RegUser(RegUser cmd)
        {

            using (var DbCtx = _factory.CreateDbContext())
            {
                var uow = new UnitOfWork(DbCtx);

                var acct = await uow.AcctRepo.FirstAsync(x => x.Addr == cmd.Addr);
                if (acct != null) throw new FBException("", "Account Already Exist");

                Acct ac = new Acct();
                ac.UsrId = await GenUserId();
                ac.Addr = cmd.Addr;
                ac.Level = 0;


                if (cmd.RefId != null)
                {
                    var refuser = await uow.AcctRepo.FirstAsync(x => x.UsrId == cmd.RefId);
                    if (refuser == null) throw new FBException("", "Referrer doesn't exist");

                    ac.RId = refuser.Id;
                    ac.RefId = refuser.UsrId;
                    ac.RefAddr = refuser.Addr;
                }
                else
                {
                    ac.RId = 0;
                    ac.RefId = 0;
                    ac.RefAddr = "";
                }

                await uow.AcctRepo.AddAsync(ac);
                await uow.CommitAsync();

                await _dbcon.SPROC("useradd", new { u_id=ac.Id, m_lev=cmd.MaxLev});

                var u = _mapper.Map<UserDto>(ac);

                return u;
            }
        }

        public async Task<UserDto> GetUser(string addr)
        {
            var acct = await _spGetUser(addr: addr);
            if (acct != null)
            {
                return acct;
            }
            else throw new FBException("", "Acct not found");

            //using (var DbCtx = _factory.CreateDbContext())
            //{
            //    var uow = new UnitOfWork(DbCtx);
            //    var acct = await uow.AcctRepo.FirstAsync(x => x.Addr == addr);
            //    var u = _mapper.Map<UserDto>(acct);
            //    return u;
            //}
        }

        public async Task<UserDto> _spGetUser(long? uid=null, string? addr=null) => await _dbcon.SPROC<UserDto>("userget", new { uid = uid, addr = addr });




        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public async Task<long> GenUserId()
        {
            string thiscode = RandomString(10);
            var cnt = await _dbcon.Count($"SELECT count(Id) FROM {Const.TblAcct} WHERE UsrId='{thiscode}';");
            if (cnt == 0 && thiscode.Substring(0, 1) != "0")
                return long.Parse(thiscode);         
            else
                return await GenUserId();
           
        }

        public async Task<FBQueRes<Acct>> GetRefs(GetRefs cmd)
        {
            using (var DbCtx = _factory.CreateDbContext())
            {
                var uow = new UnitOfWork(DbCtx);
                var itemCol = await uow.AcctRepo.GetRefsList(cmd);
                return itemCol;   
            }
        }

        public async Task<CommsDto> GetComms(string addr)
        {
            var comms = await _dbcon.SqlToEnt<CommsDto>($"SELECT * FROM {Const.TblAcct} WHERE Addr='{addr}' LIMIT 1");

            if (comms == null) throw new FBException("", "Account not found!");

            return comms;        
        }

   
    }
}
