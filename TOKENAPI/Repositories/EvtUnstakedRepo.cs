using TOKENAPI.Common;
using TOKENAPI.Domain;
using TOKENAPI.EF;

namespace TOKENAPI.Repositories
{
    public class EvtUnstakedRepo : Repository<EvtUnstaked>
    {
        private readonly DbCtx _context;
        public EvtUnstakedRepo(DbCtx context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
