using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPMG.Model.Entity
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }
}
