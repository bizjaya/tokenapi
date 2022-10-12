using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using TOKENAPI.Common;
using TOKENAPI.CQRS;
using TOKENAPI.Domain;
using TOKENAPI.EF;

namespace TOKENAPI.Repositories
{
    public class AcctRepo : Repository<Acct>
    {

        private readonly DbCtx _context;
        public AcctRepo(DbCtx context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AcctAdd(Acct entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task<FBQueRes<Acct>> GetRefsList(GetRefs p)
        {
            var query = _context.Accts.Where($"long(R{p.Level}).Equals(@0)", p.RefId).AsNoTracking(); //.Where($"string(object(R{p.Level})).Equals(@0)", p.RefId);
            if (!string.IsNullOrEmpty(p.SearchTxt))
            {
                query = query.Where($"string(object({p.SearchBy})).ToLower().Contains(@0)", p.SearchTxt.ToLower());
            }
            var result = await query.OrderBy($"{p.SortBy} {p.SortDir}").Skip(p.PageOff).Take(p.PageSize).ToListAsync();

            var queres = new FBQueRes<Acct>(query, result);

            return queres;
        }

    }
}
