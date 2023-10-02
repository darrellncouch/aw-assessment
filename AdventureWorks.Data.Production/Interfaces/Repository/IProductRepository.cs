using AdventureWorks.Data.Common;
using AdventureWorks.Data.Production.EntityFramework;

namespace AdventureWorks.Data.Production.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<Result<int>> Create(Product product);
        Task<Result> Update(Product product);
    }
}
