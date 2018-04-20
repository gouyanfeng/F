
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{


    public class BaseRepository<T> where T : class
    {

        public System.Data.Entity.DbContext db = DbContextFactory.GetCurrentDbContext();

        public T Create(T t)
        {
            return db.Set<T>().Add(t);
        }
        public List<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> fun)
        {
            return db.Set<T>().Where(fun).ToList();
        }
    }
}
