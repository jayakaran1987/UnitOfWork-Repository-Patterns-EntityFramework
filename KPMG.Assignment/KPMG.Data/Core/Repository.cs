using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.Data.Core
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; set; }
        internal DbSet<T> EntitySet { get; set; }

        public Repository(DbContext context)
        {
            Context = context;
            EntitySet = context.Set<T>();
        }

        public virtual DbSet<T> EntityContext()
        {
            return EntitySet;
        }

        public virtual IQueryable<T> GetAll()
        {
            return EntitySet.AsQueryable();
        }

        public virtual T Add(T entity)
        {
            T ret = null;
            ret = EntitySet.Add(entity);
            return ret;
        }

        public virtual T Delete(T entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                EntitySet.Attach(entityToDelete);
            }
            return EntitySet.Remove(entityToDelete);
        }

        public virtual T Update(T entityToUpdate)
        {
            try
            {
                T ret = EntitySet.Attach(entityToUpdate);
                Context.Entry(entityToUpdate).State = EntityState.Modified;
                return ret;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (T)entry.Entity;
                var databaseValues = (T)entry.GetDatabaseValues().ToObject();

                throw ex;
            }
        }
    }
}
