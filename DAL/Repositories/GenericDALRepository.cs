using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Context;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public abstract class GenericDalRepository<T> : IRepository<T>
        where T : class
    {
        protected readonly OrderContext Db;
        protected GenericDalRepository(OrderContext context)
        {
            Db = context;
        }
        public void Create(T item)
        {
            Db.Set<T>().Add(item);
        }

        public void Delete(int id)
        {
            //T item = Db.Set<T>().Find(id);
            T item =Get(id);
            if (item != null)
                Db.Set<T>().Remove(item);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public T Get(int id)
        {
            return Db.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Db.Set<T>();
        }

        public void Update(T item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
