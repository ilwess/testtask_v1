using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    class Repository<TModel> :
        IRepository<TModel> 
        where TModel : class
    {
        private DbContext db;
        private DbSet<TModel> set;

        public Repository(DbContext context)
        {
            db = context;
            set = context.Set<TModel>();
        }

        public async Task AddAsync(TModel model)
        {
            set.Add(model);
            await db.SaveChangesAsync();
        }
        public void Add(TModel model)
        {
            set.Add(model);
            db.SaveChanges();
        }

        public IEnumerable<TModel> Get()
        {
            return set.AsNoTracking();
        }
        public IEnumerable<TModel> Get(Func<TModel, bool> predicate)
        {
            return set.AsNoTracking().Where(predicate).ToList();
        }

        public void Remove(TModel model)
        {
            set.Remove(model);
            db.SaveChanges();
        }

        public void Update(TModel model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
