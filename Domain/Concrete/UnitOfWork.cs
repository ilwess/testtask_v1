using Domain.Abstract;
using Domain.Entities;
using Domain.EXContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private ShopContext db;
        private IRepository<Product> productRepos;
        private IRepository<Order> orderRepos;
        private IRepository<Customer> customerRepos;
        private bool isDisposed = false;

        public UnitOfWork(ShopContext db)
        {
            this.db = db;
        }

        public IRepository<Product> Products
        {
            get
            {
                return productRepos ?? new Repository<Product>(db)
;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                return orderRepos ?? new Repository<Order>(db)
;
            }
        }
        public IRepository<Customer> Customers
        {
            get
            {
                return customerRepos ?? new Repository<Customer>(db)
;
            }
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await db.SaveChangesAsync();
        }

        public virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    db.Dispose();
                }
                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Reset()
        {
            foreach(var entry in db.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Detached:
                        entry.Reload();
                        break;
                }
            }
        }

        public async Task ResetAsync()
        {
            foreach (var entry in db.ChangeTracker.Entries()
                            .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Detached:
                        await entry.ReloadAsync();
                        break;
                }
            }
        }
    }
}
