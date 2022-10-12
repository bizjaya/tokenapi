using Microsoft.EntityFrameworkCore.Storage;
using TOKENAPI.EF;
using TOKENAPI.Repositories;

namespace TOKENAPI.Common
{
    public interface IUnitOfWork : IDisposable
    {
        /// ADD ALL YOUR REPOSITORIES HERE

        void Commit();
        Task CommitAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbCtx _context;
        //  private readonly DbCon _dbcon;


        public UnitOfWork(DbCtx context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// ADD ALL YOUR REPOSITORIES HERE

        public AcctRepo AcctRepo => new AcctRepo(_context) ?? throw new ArgumentNullException(nameof(AcctRepo));

        public EvtApprovalRepo EvtApprovalRepo => new EvtApprovalRepo(_context) ?? throw new ArgumentNullException(nameof(EvtApprovalRepo));
        public EvtClaimedRepo EvtClaimedRepo => new EvtClaimedRepo(_context) ?? throw new ArgumentNullException(nameof(EvtClaimedRepo));
        public EvtPurchasedRepo EvtPurchasedRepo => new EvtPurchasedRepo(_context) ?? throw new ArgumentNullException(nameof(EvtPurchasedRepo));
        public EvtStakedRepo EvtStakedRepo => new EvtStakedRepo(_context) ?? throw new ArgumentNullException(nameof(EvtStakedRepo));
        public EvtTransferRepo EvtTransferRepo => new EvtTransferRepo(_context) ?? throw new ArgumentNullException(nameof(EvtTransferRepo));
        public EvtUnstakedRepo EvtUnstakedRepo => new EvtUnstakedRepo(_context) ?? throw new ArgumentNullException(nameof(EvtUnstakedRepo));
        public ProStakedRepo ProStakedRepo => new ProStakedRepo(_context) ?? throw new ArgumentNullException(nameof(ProStakedRepo));
        public ProUnstakedRepo ProUnstakedRepo => new ProUnstakedRepo(_context) ?? throw new ArgumentNullException(nameof(ProUnstakedRepo));
        public ProClaimedRepo ProClaimedRepo => new ProClaimedRepo(_context) ?? throw new ArgumentNullException(nameof(ProClaimedRepo));
        public ProRefComRepo ProRefComRepo => new ProRefComRepo(_context) ?? throw new ArgumentNullException(nameof(ProRefComRepo));




        public void Commit() => _context.SaveChanges();

        public IDbContextTransaction Transaction() => _context.Database.BeginTransaction();

        public async Task CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }


}
