using TOKENAPI.Common;
using TOKENAPI.Domain;
using TOKENAPI.EF;

namespace TOKENAPI.Repositories
{
    public class EvtClaimedRepo : Repository<EvtClaimed>
    {
        private readonly DbCtx _context;
        public EvtClaimedRepo(DbCtx context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
