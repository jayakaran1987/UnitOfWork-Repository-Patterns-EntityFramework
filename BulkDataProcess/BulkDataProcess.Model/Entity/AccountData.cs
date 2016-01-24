using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPMG.Model.Entity;

namespace BulkDataProcess.Model.Entity
{
    public class AccountData : IEntityBase
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
    }
}
