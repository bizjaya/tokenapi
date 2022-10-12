using TOKENAPI.Common;
using TOKENAPI.Domain;
using TOKENAPI.EF;

namespace TOKENAPI.Repositories
{
    public class EvtStakedRepo : Repository<EvtStaked>
    {
        private readonly DbCtx _context;
        public EvtStakedRepo(DbCtx context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
