
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{


    public class BaseRepository<T> where T : class
    {

        public System.Data.Entity.DbContext Context = DbContextFactory.GetCurrentStoreContext();

      
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public IQueryable<T> Table { get { return Context.Set<T>(); } }

        public T Create(T t)
        {
            return Context.Set<T>().Add(t);
        }
        public IQueryable<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> fun)
        {
            return Context.Set<T>().Where(fun);
        }
    }
}
