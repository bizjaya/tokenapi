using TOKENAPI.Common;
using TOKENAPI.Domain;
using TOKENAPI.EF;

public class ProClaimedRepo : Repository<ProClaimed>
{
    private readonly DbCtx _context;
    public ProClaimedRepo(DbCtx context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}