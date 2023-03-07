using Mc_Task.DTO;
using Mc_Task.Models;

namespace Mc_Task.Services
{
    public interface IProductService
    {
        int Delete(int id);
        List<Product> GetAll();
        Task<IEnumerable<Product>> Search(SearchRequest request);
        Product GetById(int id);
        int Insert(ProductViewModel product);
        int Update(int id, ProductViewModel product);
    }
}
