﻿using Domain.Abstract;
using Domain.Entities;
using Domain.EXContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class Repository<TModel> :
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
        public void Add(TModel model)
        {
            set.Add(model);
        }

        public IEnumerable<TModel> Get()
        {
            return set.AsNoTracking();
        }
        public IEnumerable<TModel> Get(Func<TModel, bool> predicate)
        {
            return set.AsNoTracking().Where(predicate);
        }

        public void Remove(TModel model)
        {
            set.Remove(model);
        }

        public void Update(TModel model)
        {
            db.Entry(model).State = EntityState.Modified;
        }

        public TModel Find(int id)
        {
            return set.Find(id);
        }

        public async Task<TModel> FindAsync(int id)
        {
            return await set.FindAsync(id);
        }

        public async Task<IEnumerable<TModel>> FindRangeAsync(params int[] ids)
        {
            var items = new List<TModel>();
            foreach(var id in ids)
            {
                items.Add(await set.FindAsync(id));
            }
            return items;
        }
    }
}
