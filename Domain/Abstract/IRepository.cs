using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IRepository<TModel> where TModel : class
    {
        IEnumerable<TModel> Get();
        IEnumerable<TModel> Get(Func<TModel, bool> predicate);

        void Remove(TModel model);

        void Update(TModel model);

        void Add(TModel model);
        Task AddAsync(TModel model);
    }
}
