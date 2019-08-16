using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IRepository<TModel> where TModel : class
    {
        IEnumerable<TModel> Get();
        IEnumerable<TModel> Get(Func<TModel, bool> predicate);

        TModel Find(int id);
        Task<TModel> FindAsync(int id);
        Task<IEnumerable<TModel>> FindRangeAsync(params int[] ids);

        void Remove(TModel model);

        void Update(TModel model);

        void Add(TModel model);
    }
}
