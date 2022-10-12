using TOKENAPI.Common;
using TOKENAPI.Domain;
using TOKENAPI.EF;

namespace TOKENAPI.Repositories
{
    public class EvtPurchasedRepo : Repository<EvtPurchased>
    {
        private readonly DbCtx _context;
        public EvtPurchasedRepo(DbCtx context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
