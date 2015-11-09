using KPMG.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Dispose();
        DbContext DBContext();
        IAccountDataRepository AccountDatas { get; }

        
    }
}
