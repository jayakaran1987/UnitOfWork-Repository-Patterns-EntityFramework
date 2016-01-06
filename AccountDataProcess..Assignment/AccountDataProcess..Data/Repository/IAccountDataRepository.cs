using AccountDataProcess.Data.Core;
using AccountDataProcess.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDataProcess.Data.Repository
{
    public interface IAccountDataRepository : IRepository<AccountData>
    {
        AccountData GetAccountDataById(Guid Id);
    }
}
