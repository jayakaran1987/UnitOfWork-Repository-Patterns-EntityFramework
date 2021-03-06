﻿using BulkDataProcess.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkDataProcess.Data
{
    public static class UowFactory
    {
        public static IUnitOfWork Create()
        {
            var factories = new RepositoryFactories();
            var provider = new RepositoryProvider(factories);

            IUnitOfWork uow = new UnitOfWork(provider);
            return uow;
        }
    }
}
