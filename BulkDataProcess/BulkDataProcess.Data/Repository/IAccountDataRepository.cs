using BulkDataProcess.Data.Core;
using BulkDataProcess.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkDataProcess.Data.Repository
{
    public interface IAccountDataRepository : IRepository<AccountData>
    {
        AccountData GetAccountDataById(Guid Id);
    }
}
