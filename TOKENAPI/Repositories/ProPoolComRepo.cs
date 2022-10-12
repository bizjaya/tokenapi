using TOKENAPI.Common;
using TOKENAPI.Domain;
using TOKENAPI.EF;

namespace TOKENAPI.Repositories
{
    public class ProPoolComRepo : Repository<ProPoolCom>
    {
        private readonly DbCtx _context;
        public ProPoolComRepo(DbCtx context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
