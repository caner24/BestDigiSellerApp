using BestDigiSellerApp.Wallet.Data.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WalletContext _context;
        private IDbContextTransaction? _transaction;
        private readonly IWalletDal _walletDal;
        private readonly IWalletDetailDal _walletDetailDal;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public UnitOfWork(WalletContext context, ClaimsPrincipal claimsPrincipal, IWalletDal walletDal, IWalletDetailDal walletDetailDal)
        {
            _context = context;
            _walletDal = walletDal;
            _walletDetailDal = walletDetailDal;
            _claimsPrincipal = claimsPrincipal;

        }

        public IWalletDal WalletDal => _walletDal;
        public IWalletDetailDal WalletDetailDal => _walletDetailDal;
        public ClaimsPrincipal ClaimsPrincipal => _claimsPrincipal;
        public WalletContext Context => _context;
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
            _transaction?.Dispose();
        }
    }
}
