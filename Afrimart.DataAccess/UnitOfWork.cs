﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.DataAccess.Repositories;
using DataAccess.Repositories;

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
        ICartRepo CartRepo { get; set; }
        ICartItemRepo CartItemRepo { get; set; }
        IAddressRepo AddressRepo { get; set; }
        IShopperProfileRepo ShopperProfileRepo { get; set; }
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
        public ICartRepo CartRepo { get; set; }
        public ICartItemRepo CartItemRepo { get; set; }
        public IAddressRepo AddressRepo { get; set; }
        public IShopperProfileRepo ShopperProfileRepo { get; set; }


        public UnitOfWork(AfrimartDbContext context)
        {
            _context = context;
            ProductCategoryRepo = new ProductCategoryRepo(_context);
            UserRepo = new UserRepo(_context);
            StoreRepo = new StoreRepo(_context);
            RoleRepo = new RoleRepo(_context);
            ProductRepo = new ProductRepo(_context);
            ProductFileRepo = new ProductFileRepo(_context);
            CartRepo = new CartRepo(_context);
            CartItemRepo = new CartItemRepo(_context);
            AddressRepo = new AddressRepo(_context);
            ShopperProfileRepo = new ShopperProfileRepo(_context);
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
