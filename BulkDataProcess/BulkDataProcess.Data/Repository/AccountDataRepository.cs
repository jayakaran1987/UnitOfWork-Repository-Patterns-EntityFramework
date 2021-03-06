﻿using BulkDataProcess.Data.Core;
using BulkDataProcess.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkDataProcess.Data.Repository
{
    public class AccountDataRepository : Repository<AccountData>, IAccountDataRepository
    {
        public AccountDataRepository(DbContext context): base(context){ }

        /* override any method to provide custom behaviour */

        public AccountData GetAccountDataById(Guid Id)
        {
            return EntitySet.Where(con => con.Id == Id).FirstOrDefault();
        }
    }
}
