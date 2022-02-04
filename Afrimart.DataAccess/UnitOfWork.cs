using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.Repositories;

namespace Afrimart.DataAccess
{
    public interface IUnitOfWork
    {
        IProductRepo ProductRepo { get; set; }
        IProductCategoryRepo ProductCategoryRepo { get; set; }
        IProductFileRepo ProductFileRepo { get; set; }
        IUserRepo UserRepo { get; set; }
        IStoreRepo StoreRepo { get; set; }
        IRoleRepo RoleRepo { get; set; }
        Task SaveChangesAsync();
    }
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AfrimartDbContext _context;
        public IProductCategoryRepo ProductCategoryRepo { get; set; }
        public IProductRepo ProductRepo { get; set; }
        public IProductFileRepo ProductFileRepo { get; set; }
        public IUserRepo UserRepo { get; set; }
        public IStoreRepo StoreRepo { get; set; }
        public IRoleRepo RoleRepo { get; set; }


        public UnitOfWork(AfrimartDbContext context)
        {
            _context = context;
            ProductCategoryRepo = new ProductCategoryRepo(_context);
            UserRepo = new UserRepo(_context);
            StoreRepo = new StoreRepo(_context);
            RoleRepo = new RoleRepo(_context);
            ProductRepo = new ProductRepo(_context);
            ProductFileRepo = new ProductFileRepo(_context);
        }

        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
