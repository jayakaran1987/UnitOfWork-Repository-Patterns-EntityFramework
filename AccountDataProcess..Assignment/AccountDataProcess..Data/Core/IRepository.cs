using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDataProcess.Data.Core
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        DbSet<T> EntityContext();
        T Add(T entity);
        T Delete(T entityToDelete);
        T Update(T entityToUpdate);

    }
}
