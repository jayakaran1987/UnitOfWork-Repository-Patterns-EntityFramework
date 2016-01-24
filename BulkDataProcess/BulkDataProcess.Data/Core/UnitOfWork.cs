using BulkDataProcess.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkDataProcess.Data.Core
{
    public class UnitOfWork: IUnitOfWork
    {
        public BulkDataProcessDbContext Context { get; set; }
        protected RepositoryProvider RepositoryProvider { get; set; }

        public UnitOfWork(RepositoryProvider repositoryProvider)
        {
            Context = new BulkDataProcessDbContext();
            repositoryProvider.DbContext = Context;
            RepositoryProvider = repositoryProvider;
        }

        public DbContext DBContext()
        {
            return Context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public IAccountDataRepository AccountDatas { get { return GetRepo<IAccountDataRepository>(); } }

        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }
        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
