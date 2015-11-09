using KPMG.Data.Core;
using KPMG.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.Data.Repository
{
    public interface IAccountDataRepository : IRepository<AccountData>
    {
        AccountData GetAccountDataById(Guid Id);
    }
}
