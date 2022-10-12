using Microsoft.EntityFrameworkCore;
using TOKENAPI.EF;
using TOKENAPI.Mapper;
using TOKENAPI.Models;

namespace TOKENAPI.Service
{
    public interface IJobService
    {
        

    }
    public class JobService : IJobService
    {

        private readonly Cont _cont;
        private readonly Stgs _stgs;

       // private readonly static IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new EvtProfile(_stgs))).CreateMapper();
        private readonly IDbContextFactory<DbCtx> _factory;
        private readonly DbCon _dbcon;


        public JobService(Cont cont, Stgs stgs, IDbContextFactory<DbCtx> factory, DbCon dbcon)
        {
            _cont = cont;
            _stgs = stgs;
            _factory = factory;
            _dbcon = dbcon;
        }




    }
}
