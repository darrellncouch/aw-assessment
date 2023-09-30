using AdventureWorks.Data.Common;
using AdventureWorks.Data.Production.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Data.Production.Interfaces.Query
{
    public interface IProductModelQuery
    {
        Task<Result<ProductModel>> GetById(int id);
    }
}
