using AccountDataProcess.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDataProcess.DapperData.Repository
{
    public interface IAccountDataRepository
    {
        List<AccountData> GetAll();
        AccountData Find(Guid Id);
        void Add(AccountData entity);
        void Update(AccountData entity);
        void Remove(Guid Id);

    }
}
