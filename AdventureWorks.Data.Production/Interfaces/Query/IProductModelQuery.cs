using AdventureWorks.Data.Common;
using AdventureWorks.Data.Production.EntityFramework;

namespace AdventureWorks.Data.Production.Interfaces.Query
{
    public interface IProductModelQuery
    {
        Task<Result<ProductModel>> GetById(int id);
        Task<Result<List<ProductModel>>> GetAll();
    }
}
